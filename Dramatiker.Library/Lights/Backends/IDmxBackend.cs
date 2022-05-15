namespace Dramatiker.Library.Lights.Backends;

public interface IDmxBackend
{
	public void SetColor(Fixture light, Color color);

	/// <summary>
	///     Sets a channels to a specific value.
	/// </summary>
	public void SetChannel(int channel, byte value);

	/// <summary>
	///     Sets all channels to a specific value.
	/// </summary>
	public void SetAllChannels(byte value);

	/// <summary>
	///     Sets all channels to 255.
	/// </summary>
	public void AllChannelsOn();

	/// <summary>
	///     Sets all channels to 0.
	/// </summary>
	public void ClearChannels();

	/// <summary>
	///     Close the connection.
	/// </summary>
	public void Close();

	/// <summary>
	///     Send the message to the device.
	/// </summary>
	public void Flush();
	
	public bool IsConnected { get; }
}