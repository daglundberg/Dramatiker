using System.IO.Ports;

namespace Dramatiker.Library.Lights.Backends;

/// <summary>
///     Controller maintains a state and interface for interacting with the Enttec
///     DMX USB Pro.
/// </summary>
public class EntecUsbPro : IDmxBackend
{
	private const byte SignalStart = 0x7E;
	private const byte SignalEnd = 0xE7;
	
	private readonly SerialPort _serialPort;

	/// <summary>Instantiate Controller.</summary>
	/// <param name="port">COM port to use for communication.</param>
	/// <param name="dmxSize">Number of channels from 24 to 512.</param>
	/// <param name="baudrate">Baudrate for serial connection.</param>
	/// <param name="timeout">Serial connection timeout.</param>
	public EntecUsbPro(string port, int dmxSize = 512, int baudrate = 57600, int timeout = 1000)
	{
		/*
		string[] PortNames = SerialPort.GetPortNames();
		Console.WriteLine("Available Ports:");
		foreach (var s in PortNames)
		{
			Console.WriteLine("   {0}", s);
		}
		*/
		
		if (dmxSize is > 512 or < 24)
		{
			Console.WriteLine(@"Size of DMX channel frame must be between 24 and 512! Defaulting to 24.");
			dmxSize = 24;
		}
		
		DmxSize = dmxSize;
		
		// Create a new SerialPort object with default settings.
		_serialPort = new SerialPort(port, baudrate);
		_serialPort.Handshake = Handshake.XOnXOff;

		// Set the read/write timeouts
		_serialPort.ReadTimeout = timeout;
		_serialPort.WriteTimeout = timeout;

		try
		{
			_serialPort.Open();
			IsConnected = true;
		}
		catch (FileNotFoundException e)
		{
			Console.WriteLine(@"Could not connect to the Enttec DMX USB Pro.");
			Console.WriteLine(e.Message);
		}
		catch (UnauthorizedAccessException e)
		{
			Console.WriteLine(@"Could not connect to the Enttec DMX USB Pro.");
			Console.WriteLine(e.Message);
		}

		Message = CreateMessage();
	}

	private int DmxSize { get; }
	private byte[] Message { get; }

	private Span<byte> Channels => new(Message, 5, DmxSize);

	public bool IsConnected { get; }

	public void SetChannel(int channel, byte value)
	{
		Channels[channel] = value;
	}

	public void SetColor(Fixture light, Color color)
	{
		var span = Channels.Slice(light.FirstChannel, 3);

		span[0] = color.R;
		span[1] = color.G;
		span[2] = color.B;
	}

	public void ClearChannels()
	{
		Channels.Clear();
	}

	public void AllChannelsOn()
	{
		Channels.Fill(255);
	}

	public void SetAllChannels(byte value)
	{
		Channels.Fill(value);
	}

	public void Close()
	{
		_serialPort.Close();
	}

	public void Flush()
	{
		if (_serialPort.IsOpen)
			_serialPort.Write(Message, 0, Message.Length);
	}

	private byte[] CreateMessage()
	{
		var message = new byte[DmxSize + 6];
		message[0] = SignalStart;
		message[1] = 6;
		message[2] = (byte) ((DmxSize + 1) & 0xFF);
		message[3] = (byte) (((DmxSize + 1) >> 8) & 0xFF);
		message[4] = 0;
		message[^1] = SignalEnd;
		return message;
	}
}