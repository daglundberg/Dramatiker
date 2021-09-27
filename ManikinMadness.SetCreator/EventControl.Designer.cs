
namespace ManikinMadness.SetCreator
{
	partial class EventControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblEventType = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblEventType
			// 
			this.lblEventType.AutoSize = true;
			this.lblEventType.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.lblEventType.Location = new System.Drawing.Point(3, 2);
			this.lblEventType.Name = "lblEventType";
			this.lblEventType.Size = new System.Drawing.Size(62, 21);
			this.lblEventType.TabIndex = 0;
			this.lblEventType.Text = "Fade In";
			// 
			// EventControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Controls.Add(this.lblEventType);
			this.Name = "EventControl";
			this.Size = new System.Drawing.Size(188, 63);
			this.Click += new System.EventHandler(this.EventControl_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblEventType;
	}
}
