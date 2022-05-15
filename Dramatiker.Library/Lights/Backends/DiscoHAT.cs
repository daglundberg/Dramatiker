using System;
using System.IO;
using System.IO.Ports;

namespace Dramatiker.Library.Lights.Backends;

public class DiscoHat : IDmxBackend
{
	private const byte SignalStart = 0x7E;
	private const byte SignalEnd = 0xE7;

	private readonly SerialPort _serialPort;
	//static SerialPortStream _serialPort;

	/// <summary>Instantiate Controller.</summary>
	/// <param name="port">COM port to use for communication.</param>
	/// <param name="dmxSize">Number of channels from 24 to 512.</param>
	public DiscoHat(string port, int dmxSize = 24)
	{
		Port = port;
		DmxSize = dmxSize;
		Baudrate = 500_000;
		Timeout = 1000;

		if (DmxSize > 512 || DmxSize < 24)
		{
			Console.WriteLine(@"Size of DMX channel frame must be between 24 and 512! Defaulting to 512.");
			DmxSize = 512;
		}

		// Create a new SerialPort object with default settings.
		_serialPort = new SerialPort(port, Baudrate);
		//_serialPort = new SerialPortStream(port, Baudrate);
		_serialPort.Handshake = Handshake.None;
		_serialPort.Parity = Parity.None;
		_serialPort.StopBits = StopBits.One;

		// Set the read/write timeouts
		_serialPort.ReadTimeout = Timeout;
		_serialPort.WriteTimeout = Timeout;

		try
		{
			_serialPort.Open();
			IsConnected = true;
		}
		catch (FileNotFoundException e)
		{
			Console.WriteLine(@"Could not connect to the serial port.");
			Console.WriteLine(e.Message);
		}
		catch (UnauthorizedAccessException e)
		{
			Console.WriteLine(@"Could not connect to the serial port.");
			Console.WriteLine(e.Message);
		}

		Message = CreateMessage();
	}

	public string Port { get; }
	public int DmxSize { get; }
	public int Baudrate { get; }
	public int Timeout { get; }
	public byte[] Message { get; private set; }

	private Span<byte> Channels => new Span<byte>(Message, 5, DmxSize);

	public void SetChannel(int channel, byte value)
	{
		Channels[channel] = value;
	}

	public void SetColor(Fixture light, Color color)
	{
		var span = Channels.Slice(light.FirstChannel, 4);

		span[0] = color.R;
		span[1] = color.G;
		span[2] = color.B;
		//span[3] = color.A;
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
		if (_serialPort.IsOpen) _serialPort.Write(Message, 0, Message.Length);
	}

	public bool IsConnected { get; } = false;

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