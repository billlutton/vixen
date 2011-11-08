﻿namespace Scheduler {
	partial class ScheduleWeek {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);

			_backgroundBrush.Dispose();
			_hourPen.Dispose();
			_halfHourPen.Dispose();
			_timeLargeFont.Dispose();
			_timeSmallFont.Dispose();
			_dayViewHeaderFont.Dispose();
			_timeLinePen.Dispose();
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// vScrollBar
			// 
			this.vScrollBar.Location = new System.Drawing.Point(208, 94);
			this.vScrollBar.Maximum = 47;
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(17, 80);
			this.vScrollBar.TabIndex = 0;
			this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
			// 
			// ScheduleWeek
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.vScrollBar);
			this.Name = "ScheduleWeek";
			this.Size = new System.Drawing.Size(255, 235);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.VScrollBar vScrollBar;
	}
}
