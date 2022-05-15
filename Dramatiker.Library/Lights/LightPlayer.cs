using Dramatiker.Library.Lights.Backends;

namespace Dramatiker.Library.Lights;

public class LightPlayer : IDisposable
{
	private readonly IDmxBackend _dmxBackend;
	private readonly Thread _thread;
	private bool _continue = true;

	public LightPlayer(IEnumerable<Fixture> lights, IDmxBackend dmxBackend)
	{
		_dmxBackend = dmxBackend;
		_thread = new Thread(() => RunLights(lights.ToArray(), _dmxBackend, ref _continue));
		_thread.Start();
	}

	public void Dispose()
	{
		_continue = false;
		_thread.Join();
		_dmxBackend.ClearChannels();
		_dmxBackend.Flush();
		_dmxBackend.Close();
	}

	public void Flush()
	{
		_dmxBackend.Flush();
	}

	private static void RunLights(IReadOnlyList<Fixture> fixtures, IDmxBackend lightController, ref bool shouldContinue)
	{
		const int fps = 40;
		const int timeStepMs = 1000 / fps;
		const float timeStepS = timeStepMs / 1000f;

		while (shouldContinue)
		{
			Thread.Sleep(timeStepMs);

			for (var i = 0; i < fixtures.Count; i++)
				lock (fixtures[i])
					lightController.SetColor(fixtures[i], fixtures[i].GetColor(timeStepS));
			
			lightController.Flush();
		}
	}
}