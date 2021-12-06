using System;
using System.Windows.Forms;
using Dramatiker.Library;

namespace Dramatiker.SetCreator
{
    public partial class PlaybackForm : Form
    {
        AudioPlayer AudioPlayer;
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

            UpdateLabels();
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
        }

        private void btnTriggerNext_Click(object sender, EventArgs e)
        {
            _set.TriggerNext(AudioPlayer);
            UpdateLabels();
        }
    }
}
