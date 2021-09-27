
namespace ManikinMadness.SetCreator
{
    partial class PlaybackForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNextEvent = new System.Windows.Forms.Label();
            this.btnTriggerNext = new System.Windows.Forms.Button();
            this.lblPrevEvent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNextEvent
            // 
            this.lblNextEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNextEvent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNextEvent.Location = new System.Drawing.Point(12, 124);
            this.lblNextEvent.Name = "lblNextEvent";
            this.lblNextEvent.Size = new System.Drawing.Size(654, 25);
            this.lblNextEvent.TabIndex = 0;
            this.lblNextEvent.Text = "Next event: Fade In \"Bla bla\"";
            this.lblNextEvent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTriggerNext
            // 
            this.btnTriggerNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnTriggerNext.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTriggerNext.Location = new System.Drawing.Point(237, 203);
            this.btnTriggerNext.Name = "btnTriggerNext";
            this.btnTriggerNext.Size = new System.Drawing.Size(205, 56);
            this.btnTriggerNext.TabIndex = 1;
            this.btnTriggerNext.Text = "Trigger Next";
            this.btnTriggerNext.UseVisualStyleBackColor = true;
            this.btnTriggerNext.Click += new System.EventHandler(this.btnTriggerNext_Click);
            // 
            // lblPrevEvent
            // 
            this.lblPrevEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrevEvent.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPrevEvent.Location = new System.Drawing.Point(12, 73);
            this.lblPrevEvent.Name = "lblPrevEvent";
            this.lblPrevEvent.Size = new System.Drawing.Size(654, 15);
            this.lblPrevEvent.TabIndex = 2;
            this.lblPrevEvent.Text = "Last event: Fade In \"Bla bla\"";
            this.lblPrevEvent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlaybackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 309);
            this.Controls.Add(this.lblPrevEvent);
            this.Controls.Add(this.btnTriggerNext);
            this.Controls.Add(this.lblNextEvent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PlaybackForm";
            this.Text = "Playback";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlaybackForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNextEvent;
        private System.Windows.Forms.Button btnTriggerNext;
        private System.Windows.Forms.Label lblPrevEvent;
    }
}