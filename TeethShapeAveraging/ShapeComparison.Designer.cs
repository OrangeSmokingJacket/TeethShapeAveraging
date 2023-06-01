
namespace TeethShapeAveraging
{
	partial class ShapeComparison
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
			this.First = new System.Windows.Forms.ListBox();
			this.ProcessDataSwitch = new System.Windows.Forms.Button();
			this.Second = new System.Windows.Forms.ListBox();
			this.Clear_First_Button = new System.Windows.Forms.Button();
			this.Clear_Second_Button = new System.Windows.Forms.Button();
			this.Start_Comparing = new System.Windows.Forms.Button();
			this.m1_First_Display = new System.Windows.Forms.PictureBox();
			this.m1_Second_Display = new System.Windows.Forms.PictureBox();
			this.M3_First_Display = new System.Windows.Forms.PictureBox();
			this.M3_Second_Display = new System.Windows.Forms.PictureBox();
			this.Match_m1 = new System.Windows.Forms.TextBox();
			this.Match_M3 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.m1_First_Display)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m1_Second_Display)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.M3_First_Display)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.M3_Second_Display)).BeginInit();
			this.SuspendLayout();
			// 
			// First
			// 
			this.First.AllowDrop = true;
			this.First.BackColor = System.Drawing.SystemColors.ControlDark;
			this.First.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.First.FormattingEnabled = true;
			this.First.HorizontalScrollbar = true;
			this.First.Location = new System.Drawing.Point(12, 12);
			this.First.Name = "First";
			this.First.Size = new System.Drawing.Size(200, 299);
			this.First.TabIndex = 1;
			this.First.DragDrop += new System.Windows.Forms.DragEventHandler(this.DropBox_DragAndDrop);
			this.First.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropBox_DragEnter);
			// 
			// ProcessDataSwitch
			// 
			this.ProcessDataSwitch.Location = new System.Drawing.Point(12, 403);
			this.ProcessDataSwitch.Name = "ProcessDataSwitch";
			this.ProcessDataSwitch.Size = new System.Drawing.Size(81, 35);
			this.ProcessDataSwitch.TabIndex = 14;
			this.ProcessDataSwitch.Text = "Change Window";
			this.ProcessDataSwitch.UseVisualStyleBackColor = true;
			this.ProcessDataSwitch.Click += new System.EventHandler(this.ProcessDataSwitch_Click);
			// 
			// Second
			// 
			this.Second.AllowDrop = true;
			this.Second.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Second.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Second.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Second.FormattingEnabled = true;
			this.Second.HorizontalScrollbar = true;
			this.Second.Location = new System.Drawing.Point(588, 12);
			this.Second.Name = "Second";
			this.Second.Size = new System.Drawing.Size(200, 299);
			this.Second.TabIndex = 15;
			this.Second.DragDrop += new System.Windows.Forms.DragEventHandler(this.Second_DragAndDrop);
			this.Second.DragEnter += new System.Windows.Forms.DragEventHandler(this.Second_DragEnter);
			// 
			// Clear_First_Button
			// 
			this.Clear_First_Button.BackColor = System.Drawing.SystemColors.Control;
			this.Clear_First_Button.Cursor = System.Windows.Forms.Cursors.Default;
			this.Clear_First_Button.Location = new System.Drawing.Point(12, 317);
			this.Clear_First_Button.Name = "Clear_First_Button";
			this.Clear_First_Button.Size = new System.Drawing.Size(200, 22);
			this.Clear_First_Button.TabIndex = 16;
			this.Clear_First_Button.Text = "Clear";
			this.Clear_First_Button.UseVisualStyleBackColor = false;
			this.Clear_First_Button.Click += new System.EventHandler(this.Clear_First_Button_Click);
			// 
			// Clear_Second_Button
			// 
			this.Clear_Second_Button.BackColor = System.Drawing.SystemColors.Control;
			this.Clear_Second_Button.Cursor = System.Windows.Forms.Cursors.Default;
			this.Clear_Second_Button.Location = new System.Drawing.Point(588, 317);
			this.Clear_Second_Button.Name = "Clear_Second_Button";
			this.Clear_Second_Button.Size = new System.Drawing.Size(200, 22);
			this.Clear_Second_Button.TabIndex = 17;
			this.Clear_Second_Button.Text = "Clear";
			this.Clear_Second_Button.UseVisualStyleBackColor = false;
			this.Clear_Second_Button.Click += new System.EventHandler(this.Clear_Second_Button_Click);
			// 
			// Start_Comparing
			// 
			this.Start_Comparing.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Start_Comparing.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Start_Comparing.Location = new System.Drawing.Point(707, 403);
			this.Start_Comparing.Name = "Start_Comparing";
			this.Start_Comparing.Size = new System.Drawing.Size(81, 35);
			this.Start_Comparing.TabIndex = 18;
			this.Start_Comparing.Text = "Compare";
			this.Start_Comparing.UseVisualStyleBackColor = false;
			this.Start_Comparing.Click += new System.EventHandler(this.Start_Comparing_Click);
			// 
			// m1_First_Display
			// 
			this.m1_First_Display.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.m1_First_Display.Location = new System.Drawing.Point(218, 12);
			this.m1_First_Display.Name = "m1_First_Display";
			this.m1_First_Display.Size = new System.Drawing.Size(128, 128);
			this.m1_First_Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.m1_First_Display.TabIndex = 19;
			this.m1_First_Display.TabStop = false;
			// 
			// m1_Second_Display
			// 
			this.m1_Second_Display.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.m1_Second_Display.Location = new System.Drawing.Point(454, 12);
			this.m1_Second_Display.Name = "m1_Second_Display";
			this.m1_Second_Display.Size = new System.Drawing.Size(128, 128);
			this.m1_Second_Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.m1_Second_Display.TabIndex = 20;
			this.m1_Second_Display.TabStop = false;
			// 
			// M3_First_Display
			// 
			this.M3_First_Display.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.M3_First_Display.Location = new System.Drawing.Point(218, 146);
			this.M3_First_Display.Name = "M3_First_Display";
			this.M3_First_Display.Size = new System.Drawing.Size(128, 128);
			this.M3_First_Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.M3_First_Display.TabIndex = 21;
			this.M3_First_Display.TabStop = false;
			// 
			// M3_Second_Display
			// 
			this.M3_Second_Display.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.M3_Second_Display.Location = new System.Drawing.Point(454, 146);
			this.M3_Second_Display.Name = "M3_Second_Display";
			this.M3_Second_Display.Size = new System.Drawing.Size(128, 128);
			this.M3_Second_Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.M3_Second_Display.TabIndex = 22;
			this.M3_Second_Display.TabStop = false;
			// 
			// Match_m1
			// 
			this.Match_m1.Location = new System.Drawing.Point(364, 67);
			this.Match_m1.Name = "Match_m1";
			this.Match_m1.Size = new System.Drawing.Size(71, 20);
			this.Match_m1.TabIndex = 23;
			// 
			// Match_M3
			// 
			this.Match_M3.Location = new System.Drawing.Point(364, 202);
			this.Match_M3.Name = "Match_M3";
			this.Match_M3.Size = new System.Drawing.Size(71, 20);
			this.Match_M3.TabIndex = 24;
			// 
			// ShapeComparison
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Match_M3);
			this.Controls.Add(this.Match_m1);
			this.Controls.Add(this.M3_Second_Display);
			this.Controls.Add(this.M3_First_Display);
			this.Controls.Add(this.m1_Second_Display);
			this.Controls.Add(this.m1_First_Display);
			this.Controls.Add(this.Start_Comparing);
			this.Controls.Add(this.Clear_Second_Button);
			this.Controls.Add(this.Clear_First_Button);
			this.Controls.Add(this.Second);
			this.Controls.Add(this.ProcessDataSwitch);
			this.Controls.Add(this.First);
			this.Name = "ShapeComparison";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Text = "ShapeComparison";
			((System.ComponentModel.ISupportInitialize)(this.m1_First_Display)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m1_Second_Display)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.M3_First_Display)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.M3_Second_Display)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox First;
		private System.Windows.Forms.Button ProcessDataSwitch;
		private System.Windows.Forms.ListBox Second;
		private System.Windows.Forms.Button Clear_First_Button;
		private System.Windows.Forms.Button Clear_Second_Button;
		private System.Windows.Forms.Button Start_Comparing;
		private System.Windows.Forms.PictureBox m1_First_Display;
		private System.Windows.Forms.PictureBox m1_Second_Display;
		private System.Windows.Forms.PictureBox M3_First_Display;
		private System.Windows.Forms.PictureBox M3_Second_Display;
		private System.Windows.Forms.TextBox Match_m1;
		private System.Windows.Forms.TextBox Match_M3;
	}
}