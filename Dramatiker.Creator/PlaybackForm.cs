﻿using System;
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
            label1.Text = $"({ _set.CurrentIndex}/{ _set.Events.Count})";
            lblNextEvent.Text = "-";
            lblPrevEvent.Text = "-";

            if (_set.GetNextEvent() != null)
            {
                lblNextEvent.Text = "Next: " + _set.GetNextEvent().GetTitle() + " " + _set.GetNextEvent().GetDescription().Replace("\n", " ");
                btnTriggerNext.Text = $"Trigger Next...";
            }
            else
            {
                btnTriggerNext.Enabled = false;
            }

            if (_set.GetPreviousEvent() != null)
            {
                lblPrevEvent.Text = "Last: " + _set.GetPreviousEvent().GetTitle() + " " + _set.GetPreviousEvent().GetDescription().Replace("\n", " ");
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
