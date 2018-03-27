namespace Sound
{
    partial class Form1
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
            this.getDevicesBtn = new System.Windows.Forms.Button();
            this.logRtb = new System.Windows.Forms.RichTextBox();
            this.playSoundBtn = new System.Windows.Forms.Button();
            this.startRecordingBtn = new System.Windows.Forms.Button();
            this.stopRecordingBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // getDevicesBtn
            // 
            this.getDevicesBtn.Location = new System.Drawing.Point(314, 28);
            this.getDevicesBtn.Name = "getDevicesBtn";
            this.getDevicesBtn.Size = new System.Drawing.Size(75, 23);
            this.getDevicesBtn.TabIndex = 0;
            this.getDevicesBtn.Text = "Devices";
            this.getDevicesBtn.UseVisualStyleBackColor = true;
            this.getDevicesBtn.Click += new System.EventHandler(this.getDevicesBtn_Click);
            // 
            // logRtb
            // 
            this.logRtb.Location = new System.Drawing.Point(12, 12);
            this.logRtb.Name = "logRtb";
            this.logRtb.Size = new System.Drawing.Size(254, 572);
            this.logRtb.TabIndex = 1;
            this.logRtb.Text = "";
            // 
            // playSoundBtn
            // 
            this.playSoundBtn.Location = new System.Drawing.Point(314, 57);
            this.playSoundBtn.Name = "playSoundBtn";
            this.playSoundBtn.Size = new System.Drawing.Size(75, 23);
            this.playSoundBtn.TabIndex = 2;
            this.playSoundBtn.Text = "Play";
            this.playSoundBtn.UseVisualStyleBackColor = true;
            this.playSoundBtn.Click += new System.EventHandler(this.playSoundBtn_Click);
            // 
            // startRecordingBtn
            // 
            this.startRecordingBtn.Location = new System.Drawing.Point(353, 135);
            this.startRecordingBtn.Name = "startRecordingBtn";
            this.startRecordingBtn.Size = new System.Drawing.Size(75, 23);
            this.startRecordingBtn.TabIndex = 3;
            this.startRecordingBtn.Text = "Rec start";
            this.startRecordingBtn.UseVisualStyleBackColor = true;
            this.startRecordingBtn.Click += new System.EventHandler(this.startRecordingBtn_Click);
            // 
            // stopRecordingBtn
            // 
            this.stopRecordingBtn.Location = new System.Drawing.Point(434, 135);
            this.stopRecordingBtn.Name = "stopRecordingBtn";
            this.stopRecordingBtn.Size = new System.Drawing.Size(75, 23);
            this.stopRecordingBtn.TabIndex = 4;
            this.stopRecordingBtn.Text = "Rec stop";
            this.stopRecordingBtn.UseVisualStyleBackColor = true;
            this.stopRecordingBtn.Click += new System.EventHandler(this.stopRecordingBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(395, 57);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 5;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 596);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.stopRecordingBtn);
            this.Controls.Add(this.startRecordingBtn);
            this.Controls.Add(this.playSoundBtn);
            this.Controls.Add(this.logRtb);
            this.Controls.Add(this.getDevicesBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getDevicesBtn;
        private System.Windows.Forms.RichTextBox logRtb;
        private System.Windows.Forms.Button playSoundBtn;
        private System.Windows.Forms.Button startRecordingBtn;
        private System.Windows.Forms.Button stopRecordingBtn;
        private System.Windows.Forms.Button stopBtn;
    }
}

