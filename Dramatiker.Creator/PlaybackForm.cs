using System;
using System.Windows.Forms;
using Dramatiker.Creator;
using Dramatiker.Library;
using Dramatiker.Library.Lights;
using Dramatiker.Library.Lights.Backends;

namespace Dramatiker.SetCreator
{
    public partial class PlaybackForm : Form
    {
        AudioPlayer AudioPlayer;
        LightPlayer LightPlayer;

        private PlaybackForm()
        {
            InitializeComponent();
        }

        private Set _set;
        public PlaybackForm(Set set)
        {
            AudioPlayer = new AudioPlayer();
            AudioPlayer.PlayStartUpSound();
            InitializeComponent();
            _set = set;

            Fixture light1 = new Fixture() { FirstChannel = 0, Name = "Light 1" };
            Fixture light2 = new Fixture() { FirstChannel = 4, Name = "Light 2" };
            var lights = new Fixture[] { light1, light2 };
            UpdateLabels();

            IDmxBackend backend;
            if (MessageBox.Show("Want to use a screen-only dmx-backend?", "Choose backend...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                backend = new DummyBackend((lights.Length * 4 > 24) ? lights.Length * 4 : 24);
                DummyBackendForm f = new DummyBackendForm((DummyBackend)backend);
                f.Show();
            }
            else
                backend = new EntecUsbPro("COM3", (lights.Length * 4 > 24) ? lights.Length * 4 : 24);

            LightPlayer = new LightPlayer(lights, backend);
        }

		private void UpdateLabels()
        {
            label1.Text = $"({ _set.CurrentIndex}/{ _set.Cues.Count})";
            lblNextEvent.Text = "-";
            lblPrevEvent.Text = "-";

            if (_set.GetNextCue() != null)
            {
                lblNextEvent.Text = "Next: " + _set.GetNextCue().GetTitle() + " " + _set.GetNextCue().GetDescription().Replace("\n", " ");
                btnTriggerNext.Text = $"Trigger Next...";
            }
            else
            {
                btnTriggerNext.Enabled = false;
            }

            if (_set.GetPreviousCue() != null)
            {
                lblPrevEvent.Text = "Last: " + _set.GetPreviousCue().GetTitle() + " " + _set.GetPreviousCue().GetDescription().Replace("\n", " ");
            }
        }

        private void PlaybackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AudioPlayer.Dispose();
            LightPlayer.Dispose();
        }

        private void btnTriggerNext_Click(object sender, EventArgs e)
        {
            _set.TriggerNext(AudioPlayer, LightPlayer);

            UpdateLabels();
        }
    }
}
