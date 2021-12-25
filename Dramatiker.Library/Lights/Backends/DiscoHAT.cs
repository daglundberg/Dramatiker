using Dramatiker.Library.Lights;
using System;
//using System.IO.Ports;
using RJCP.IO.Ports;

namespace Dramatiker.Library.Lights.Backends
{
	public class DiscoHat : IDmxBackend
	{
		public string Port { get; private set; }
		public int DmxSize { get; private set; }
		public int Baudrate { get; private set; }
		public int Timeout { get; private set; }
		public byte[] Message { get; private set; }

		const byte SignalStart = 0x7E;
		const byte SignalEnd = 0xE7;

		//static SerialPort _serialPort;
		static SerialPortStream _serialPort;

		/// <summary>Instantiate Controller.</summary>
		/// <param name="port">COM port to use for communication.</param>
		/// <param name="dmxSize">Number of channels from 24 to 512.</param>
		public DiscoHat(string port, int dmxSize = 24)
		{
			Port = port;
			DmxSize = dmxSize;
			Baudrate = 230400;
			Timeout = 1000;

			if (DmxSize > 512 || DmxSize < 24)
			{
				Console.WriteLine("Size of DMX channel frame must be between 24 and 512! Defaulting to 512.");
				DmxSize = 512;
			}

			// Create a new SerialPort object with default settings.
			//_serialPort = new SerialPort(port, Baudrate);
			_serialPort = new SerialPortStream(port, Baudrate);
			_serialPort.Handshake = Handshake.None;
			_serialPort.Parity = Parity.None;
			_serialPort.StopBits = StopBits.One;
			
			// Set the read/write timeouts
			_serialPort.ReadTimeout = Timeout;
			_serialPort.WriteTimeout = Timeout;

			try
			{
				_serialPort.Open();
			}
			catch (System.IO.FileNotFoundException e)
			{
				Console.WriteLine("Could not connect to the serial port.");
				Console.WriteLine(e.Message);
			}

			PrepareMessage();
		}

		private void PrepareMessage()
		{
			Message = new byte[DmxSize + 6];
			Message[0] = SignalStart;
			Message[1] = 6;
			Message[2] = (byte)((DmxSize + 1) & 0xFF);
			Message[3] = (byte)(((DmxSize + 1) >> 8) & 0xFF);
			Message[4] = 0;
			Message[Message.Length - 1] = SignalEnd;
		}

		private Span<byte> Channels
		{
			get
			{
				return new Span<byte>(Message, 5, DmxSize);
			}
		}

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
			if (_serialPort.IsOpen)
			{
				_serialPort.Write(Message, 0, Message.Length);
			}
		}
	}
}
