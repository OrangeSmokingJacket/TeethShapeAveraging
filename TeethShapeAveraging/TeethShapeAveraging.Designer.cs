
namespace TeethShapeAveraging
{
	partial class TeethShapeAveraging
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
			this.DropBox = new System.Windows.Forms.ListBox();
			this.m1_Display = new System.Windows.Forms.PictureBox();
			this.Previous_Picture_m1_Button = new System.Windows.Forms.Button();
			this.Next_Picture_m1_Button = new System.Windows.Forms.Button();
			this.Process_Images_Button = new System.Windows.Forms.Button();
			this.progress_Bar = new System.Windows.Forms.ProgressBar();
			this.Sample_Name_Box = new System.Windows.Forms.TextBox();
			this.progress_Bar_Textbox = new System.Windows.Forms.TextBox();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.panel1 = new System.Windows.Forms.Panel();
			this.Clear_DropBox_Button = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.MarginDisplayText = new System.Windows.Forms.TextBox();
			this.MarginBar = new System.Windows.Forms.TrackBar();
			this.Previous_Picture_M3_Button = new System.Windows.Forms.Button();
			this.Next_Picture_M3_Button = new System.Windows.Forms.Button();
			this.Save_Button = new System.Windows.Forms.Button();
			this.M3_Display = new System.Windows.Forms.PictureBox();
			this.CompareShapesSwitch = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.m1_Display)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MarginBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.M3_Display)).BeginInit();
			this.SuspendLayout();
			// 
			// DropBox
			// 
			this.DropBox.AllowDrop = true;
			this.DropBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DropBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.DropBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DropBox.FormattingEnabled = true;
			this.DropBox.HorizontalScrollbar = true;
			this.DropBox.Location = new System.Drawing.Point(14, 38);
			this.DropBox.Name = "DropBox";
			this.DropBox.Size = new System.Drawing.Size(200, 299);
			this.DropBox.TabIndex = 0;
			// 
			// m1_Display
			// 
			this.m1_Display.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.m1_Display.Location = new System.Drawing.Point(22, 81);
			this.m1_Display.Name = "m1_Display";
			this.m1_Display.Size = new System.Drawing.Size(256, 256);
			this.m1_Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.m1_Display.TabIndex = 1;
			this.m1_Display.TabStop = false;
			// 
			// Previous_Picture_m1_Button
			// 
			this.Previous_Picture_m1_Button.Location = new System.Drawing.Point(22, 38);
			this.Previous_Picture_m1_Button.Name = "Previous_Picture_m1_Button";
			this.Previous_Picture_m1_Button.Size = new System.Drawing.Size(81, 35);
			this.Previous_Picture_m1_Button.TabIndex = 2;
			this.Previous_Picture_m1_Button.Text = "Previous Picture";
			this.Previous_Picture_m1_Button.UseVisualStyleBackColor = true;
			this.Previous_Picture_m1_Button.Click += new System.EventHandler(this.Previous_Picture_m1_Button_Click);
			// 
			// Next_Picture_m1_Button
			// 
			this.Next_Picture_m1_Button.Location = new System.Drawing.Point(197, 38);
			this.Next_Picture_m1_Button.Name = "Next_Picture_m1_Button";
			this.Next_Picture_m1_Button.Size = new System.Drawing.Size(81, 35);
			this.Next_Picture_m1_Button.TabIndex = 3;
			this.Next_Picture_m1_Button.Text = "Next Picture";
			this.Next_Picture_m1_Button.UseVisualStyleBackColor = true;
			this.Next_Picture_m1_Button.Click += new System.EventHandler(this.Next_Picture_m1_Button_Click);
			// 
			// Process_Images_Button
			// 
			this.Process_Images_Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Process_Images_Button.Cursor = System.Windows.Forms.Cursors.Default;
			this.Process_Images_Button.Location = new System.Drawing.Point(14, 343);
			this.Process_Images_Button.Name = "Process_Images_Button";
			this.Process_Images_Button.Size = new System.Drawing.Size(81, 35);
			this.Process_Images_Button.TabIndex = 4;
			this.Process_Images_Button.Text = "Process Images";
			this.Process_Images_Button.UseVisualStyleBackColor = false;
			this.Process_Images_Button.Click += new System.EventHandler(this.Process_Images_Button_Click);
			// 
			// progress_Bar
			// 
			this.progress_Bar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.progress_Bar.Location = new System.Drawing.Point(623, 508);
			this.progress_Bar.Name = "progress_Bar";
			this.progress_Bar.Size = new System.Drawing.Size(200, 20);
			this.progress_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progress_Bar.TabIndex = 5;
			// 
			// Sample_Name_Box
			// 
			this.Sample_Name_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Sample_Name_Box.BackColor = System.Drawing.SystemColors.Menu;
			this.Sample_Name_Box.Location = new System.Drawing.Point(14, 12);
			this.Sample_Name_Box.Name = "Sample_Name_Box";
			this.Sample_Name_Box.Size = new System.Drawing.Size(200, 20);
			this.Sample_Name_Box.TabIndex = 6;
			this.Sample_Name_Box.Text = "Sample Name";
			this.Sample_Name_Box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// progress_Bar_Textbox
			// 
			this.progress_Bar_Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.progress_Bar_Textbox.BackColor = System.Drawing.SystemColors.Menu;
			this.progress_Bar_Textbox.Enabled = false;
			this.progress_Bar_Textbox.Location = new System.Drawing.Point(623, 482);
			this.progress_Bar_Textbox.Name = "progress_Bar_Textbox";
			this.progress_Bar_Textbox.ReadOnly = true;
			this.progress_Bar_Textbox.Size = new System.Drawing.Size(200, 20);
			this.progress_Bar_Textbox.TabIndex = 7;
			this.progress_Bar_Textbox.Text = "Progress Bar";
			this.progress_Bar_Textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.Clear_DropBox_Button);
			this.panel1.Controls.Add(this.Process_Images_Button);
			this.panel1.Controls.Add(this.Sample_Name_Box);
			this.panel1.Controls.Add(this.DropBox);
			this.panel1.Location = new System.Drawing.Point(609, 42);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(224, 431);
			this.panel1.TabIndex = 11;
			// 
			// Clear_DropBox_Button
			// 
			this.Clear_DropBox_Button.BackColor = System.Drawing.SystemColors.Control;
			this.Clear_DropBox_Button.Cursor = System.Windows.Forms.Cursors.Default;
			this.Clear_DropBox_Button.Location = new System.Drawing.Point(133, 342);
			this.Clear_DropBox_Button.Name = "Clear_DropBox_Button";
			this.Clear_DropBox_Button.Size = new System.Drawing.Size(81, 35);
			this.Clear_DropBox_Button.TabIndex = 7;
			this.Clear_DropBox_Button.Text = "Clear DropBox";
			this.Clear_DropBox_Button.UseVisualStyleBackColor = false;
			this.Clear_DropBox_Button.Click += new System.EventHandler(this.Clear_DropBox_Button_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.MarginDisplayText);
			this.panel2.Controls.Add(this.MarginBar);
			this.panel2.Controls.Add(this.Previous_Picture_M3_Button);
			this.panel2.Controls.Add(this.Next_Picture_M3_Button);
			this.panel2.Controls.Add(this.Save_Button);
			this.panel2.Controls.Add(this.M3_Display);
			this.panel2.Controls.Add(this.m1_Display);
			this.panel2.Controls.Add(this.Previous_Picture_m1_Button);
			this.panel2.Controls.Add(this.Next_Picture_m1_Button);
			this.panel2.Location = new System.Drawing.Point(12, 42);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(563, 431);
			this.panel2.TabIndex = 13;
			// 
			// MarginDisplayText
			// 
			this.MarginDisplayText.Enabled = false;
			this.MarginDisplayText.Location = new System.Drawing.Point(459, 377);
			this.MarginDisplayText.Name = "MarginDisplayText";
			this.MarginDisplayText.Size = new System.Drawing.Size(81, 20);
			this.MarginDisplayText.TabIndex = 16;
			this.MarginDisplayText.Text = "Margin: 0";
			this.MarginDisplayText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// MarginBar
			// 
			this.MarginBar.Location = new System.Drawing.Point(284, 342);
			this.MarginBar.Maximum = 100;
			this.MarginBar.Name = "MarginBar";
			this.MarginBar.Size = new System.Drawing.Size(256, 45);
			this.MarginBar.TabIndex = 17;
			this.MarginBar.Scroll += new System.EventHandler(this.MarginBar_Scroll);
			// 
			// Previous_Picture_M3_Button
			// 
			this.Previous_Picture_M3_Button.Location = new System.Drawing.Point(284, 40);
			this.Previous_Picture_M3_Button.Name = "Previous_Picture_M3_Button";
			this.Previous_Picture_M3_Button.Size = new System.Drawing.Size(81, 35);
			this.Previous_Picture_M3_Button.TabIndex = 13;
			this.Previous_Picture_M3_Button.Text = "Previous Picture";
			this.Previous_Picture_M3_Button.UseVisualStyleBackColor = true;
			this.Previous_Picture_M3_Button.Click += new System.EventHandler(this.Previous_Picture_M3_Button_Click);
			// 
			// Next_Picture_M3_Button
			// 
			this.Next_Picture_M3_Button.Location = new System.Drawing.Point(459, 40);
			this.Next_Picture_M3_Button.Name = "Next_Picture_M3_Button";
			this.Next_Picture_M3_Button.Size = new System.Drawing.Size(81, 35);
			this.Next_Picture_M3_Button.TabIndex = 14;
			this.Next_Picture_M3_Button.Text = "Next Picture";
			this.Next_Picture_M3_Button.UseVisualStyleBackColor = true;
			this.Next_Picture_M3_Button.Click += new System.EventHandler(this.Next_Picture_M3_Button_Click);
			// 
			// Save_Button
			// 
			this.Save_Button.Location = new System.Drawing.Point(22, 343);
			this.Save_Button.Name = "Save_Button";
			this.Save_Button.Size = new System.Drawing.Size(81, 39);
			this.Save_Button.TabIndex = 12;
			this.Save_Button.Text = "Save Samples";
			this.Save_Button.UseVisualStyleBackColor = true;
			this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
			// 
			// M3_Display
			// 
			this.M3_Display.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.M3_Display.Location = new System.Drawing.Point(284, 81);
			this.M3_Display.Name = "M3_Display";
			this.M3_Display.Size = new System.Drawing.Size(256, 256);
			this.M3_Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.M3_Display.TabIndex = 11;
			this.M3_Display.TabStop = false;
			// 
			// CompareShapesSwitch
			// 
			this.CompareShapesSwitch.Location = new System.Drawing.Point(12, 493);
			this.CompareShapesSwitch.Name = "CompareShapesSwitch";
			this.CompareShapesSwitch.Size = new System.Drawing.Size(81, 35);
			this.CompareShapesSwitch.TabIndex = 13;
			this.CompareShapesSwitch.Text = "Change Window";
			this.CompareShapesSwitch.UseVisualStyleBackColor = true;
			this.CompareShapesSwitch.Click += new System.EventHandler(this.CompareShapesSwitch_Click);
			// 
			// TeethShapeAveraging
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(845, 540);
			this.Controls.Add(this.CompareShapesSwitch);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.progress_Bar_Textbox);
			this.Controls.Add(this.progress_Bar);
			this.Name = "TeethShapeAveraging";
			this.Text = "TeethShapeAveraging";
			((System.ComponentModel.ISupportInitialize)(this.m1_Display)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MarginBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.M3_Display)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox DropBox;
		private System.Windows.Forms.PictureBox m1_Display;
		private System.Windows.Forms.Button Previous_Picture_m1_Button;
		private System.Windows.Forms.Button Next_Picture_m1_Button;
		private System.Windows.Forms.Button Process_Images_Button;
		private System.Windows.Forms.ProgressBar progress_Bar;
		private System.Windows.Forms.TextBox Sample_Name_Box;
		private System.Windows.Forms.TextBox progress_Bar_Textbox;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button Clear_DropBox_Button;
		private System.Windows.Forms.PictureBox M3_Display;
		private System.Windows.Forms.Button Save_Button;
		private System.Windows.Forms.Button CompareShapesSwitch;
		private System.Windows.Forms.Button Previous_Picture_M3_Button;
		private System.Windows.Forms.Button Next_Picture_M3_Button;
		private System.Windows.Forms.TrackBar MarginBar;
		private System.Windows.Forms.TextBox MarginDisplayText;
	}
}

