namespace ImageConverter
{
	partial class Main
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
			if(disposing && (components != null))
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
			this.PictureBox = new System.Windows.Forms.PictureBox();
			this.OpenBtn = new System.Windows.Forms.Button();
			this.SaveBtn = new System.Windows.Forms.Button();
			this.StatusLbl = new System.Windows.Forms.Label();
			this.SaveFormatCb = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// PictureBox
			// 
			this.PictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PictureBox.Location = new System.Drawing.Point(122, 12);
			this.PictureBox.Name = "PictureBox";
			this.PictureBox.Size = new System.Drawing.Size(300, 300);
			this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.PictureBox.TabIndex = 1;
			this.PictureBox.TabStop = false;
			this.PictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PictureBox_DragDrop);
			this.PictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PictureBox_DragEnter);
			// 
			// OpenBtn
			// 
			this.OpenBtn.Location = new System.Drawing.Point(12, 12);
			this.OpenBtn.Name = "OpenBtn";
			this.OpenBtn.Size = new System.Drawing.Size(104, 23);
			this.OpenBtn.TabIndex = 2;
			this.OpenBtn.Text = "Open...";
			this.OpenBtn.UseVisualStyleBackColor = true;
			this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
			// 
			// SaveBtn
			// 
			this.SaveBtn.Location = new System.Drawing.Point(12, 68);
			this.SaveBtn.Name = "SaveBtn";
			this.SaveBtn.Size = new System.Drawing.Size(104, 23);
			this.SaveBtn.TabIndex = 3;
			this.SaveBtn.Text = "Save...";
			this.SaveBtn.UseVisualStyleBackColor = true;
			this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
			// 
			// StatusLbl
			// 
			this.StatusLbl.AutoSize = true;
			this.StatusLbl.Location = new System.Drawing.Point(119, 315);
			this.StatusLbl.Name = "StatusLbl";
			this.StatusLbl.Size = new System.Drawing.Size(35, 13);
			this.StatusLbl.TabIndex = 4;
			this.StatusLbl.Text = "label1";
			// 
			// SaveFormatCb
			// 
			this.SaveFormatCb.DisplayMember = "1";
			this.SaveFormatCb.FormattingEnabled = true;
			this.SaveFormatCb.Items.AddRange(new object[] {
			"*.bmp",
			"*.jpg",
			"*.gif",
			"*.tif"});
			this.SaveFormatCb.Location = new System.Drawing.Point(12, 41);
			this.SaveFormatCb.Name = "SaveFormatCb";
			this.SaveFormatCb.Size = new System.Drawing.Size(104, 21);
			this.SaveFormatCb.TabIndex = 5;
			this.SaveFormatCb.ValueMember = "1";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 339);
			this.Controls.Add(this.SaveFormatCb);
			this.Controls.Add(this.StatusLbl);
			this.Controls.Add(this.SaveBtn);
			this.Controls.Add(this.OpenBtn);
			this.Controls.Add(this.PictureBox);
			this.MaximumSize = new System.Drawing.Size(450, 378);
			this.MinimumSize = new System.Drawing.Size(450, 378);
			this.Name = "Main";
			this.ShowIcon = false;
			this.Text = "Image Converter";
			((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox PictureBox;
		private System.Windows.Forms.Button OpenBtn;
		private System.Windows.Forms.Button SaveBtn;
		private System.Windows.Forms.Label StatusLbl;
		private System.Windows.Forms.ComboBox SaveFormatCb;
	}
}

