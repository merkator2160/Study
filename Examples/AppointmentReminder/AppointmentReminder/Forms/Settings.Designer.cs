namespace AppointmentReminder.Forms
{
    partial class Settings
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
            this.TwilioGb = new System.Windows.Forms.GroupBox();
            this.TwilioAuthTokenTb = new System.Windows.Forms.TextBox();
            this.TwilioPhoneNumberMtb = new System.Windows.Forms.MaskedTextBox();
            this.TwilioAccountSidMtb = new System.Windows.Forms.MaskedTextBox();
            this.TwilioPhoneNumberLbl = new System.Windows.Forms.Label();
            this.TwilioAuthTokenLbl = new System.Windows.Forms.Label();
            this.TwilioAccountSidLbl = new System.Windows.Forms.Label();
            this.EmailGb = new System.Windows.Forms.GroupBox();
            this.EmailPasswordTb = new System.Windows.Forms.TextBox();
            this.EmailPortTb = new System.Windows.Forms.TextBox();
            this.EmailSmtpServerTb = new System.Windows.Forms.TextBox();
            this.EmailUseSslCb = new System.Windows.Forms.CheckBox();
            this.EmailLoginTb = new System.Windows.Forms.TextBox();
            this.EmailPortTLbl = new System.Windows.Forms.Label();
            this.EmailSmtpServerLbl = new System.Windows.Forms.Label();
            this.EmailUseSslLbl = new System.Windows.Forms.Label();
            this.EmailPasswordLbl = new System.Windows.Forms.Label();
            this.EmailLoginLbl = new System.Windows.Forms.Label();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.TwilioGb.SuspendLayout();
            this.EmailGb.SuspendLayout();
            this.SuspendLayout();
            // 
            // TwilioGb
            // 
            this.TwilioGb.Controls.Add(this.TwilioAuthTokenTb);
            this.TwilioGb.Controls.Add(this.TwilioPhoneNumberMtb);
            this.TwilioGb.Controls.Add(this.TwilioAccountSidMtb);
            this.TwilioGb.Controls.Add(this.TwilioPhoneNumberLbl);
            this.TwilioGb.Controls.Add(this.TwilioAuthTokenLbl);
            this.TwilioGb.Controls.Add(this.TwilioAccountSidLbl);
            this.TwilioGb.Location = new System.Drawing.Point(12, 12);
            this.TwilioGb.Name = "TwilioGb";
            this.TwilioGb.Size = new System.Drawing.Size(541, 144);
            this.TwilioGb.TabIndex = 0;
            this.TwilioGb.TabStop = false;
            this.TwilioGb.Text = "Twilio";
            // 
            // TwilioAuthTokenTb
            // 
            this.TwilioAuthTokenTb.Location = new System.Drawing.Point(6, 71);
            this.TwilioAuthTokenTb.Name = "TwilioAuthTokenTb";
            this.TwilioAuthTokenTb.Size = new System.Drawing.Size(529, 20);
            this.TwilioAuthTokenTb.TabIndex = 9;
            this.TwilioAuthTokenTb.UseSystemPasswordChar = true;
            // 
            // TwilioPhoneNumberMtb
            // 
            this.TwilioPhoneNumberMtb.Location = new System.Drawing.Point(6, 110);
            this.TwilioPhoneNumberMtb.Mask = "+9990000000";
            this.TwilioPhoneNumberMtb.Name = "TwilioPhoneNumberMtb";
            this.TwilioPhoneNumberMtb.Size = new System.Drawing.Size(529, 20);
            this.TwilioPhoneNumberMtb.TabIndex = 8;
            // 
            // TwilioAccountSidMtb
            // 
            this.TwilioAccountSidMtb.Location = new System.Drawing.Point(6, 32);
            this.TwilioAccountSidMtb.Name = "TwilioAccountSidMtb";
            this.TwilioAccountSidMtb.Size = new System.Drawing.Size(529, 20);
            this.TwilioAccountSidMtb.TabIndex = 6;
            this.TwilioAccountSidMtb.UseSystemPasswordChar = true;
            // 
            // TwilioPhoneNumberLbl
            // 
            this.TwilioPhoneNumberLbl.AutoSize = true;
            this.TwilioPhoneNumberLbl.Location = new System.Drawing.Point(12, 94);
            this.TwilioPhoneNumberLbl.Name = "TwilioPhoneNumberLbl";
            this.TwilioPhoneNumberLbl.Size = new System.Drawing.Size(75, 13);
            this.TwilioPhoneNumberLbl.TabIndex = 2;
            this.TwilioPhoneNumberLbl.Text = "Twilio number:";
            // 
            // TwilioAuthTokenLbl
            // 
            this.TwilioAuthTokenLbl.AutoSize = true;
            this.TwilioAuthTokenLbl.Location = new System.Drawing.Point(12, 55);
            this.TwilioAuthTokenLbl.Name = "TwilioAuthTokenLbl";
            this.TwilioAuthTokenLbl.Size = new System.Drawing.Size(62, 13);
            this.TwilioAuthTokenLbl.TabIndex = 1;
            this.TwilioAuthTokenLbl.Text = "Auth token:";
            // 
            // TwilioAccountSidLbl
            // 
            this.TwilioAccountSidLbl.AutoSize = true;
            this.TwilioAccountSidLbl.Location = new System.Drawing.Point(12, 16);
            this.TwilioAccountSidLbl.Name = "TwilioAccountSidLbl";
            this.TwilioAccountSidLbl.Size = new System.Drawing.Size(66, 13);
            this.TwilioAccountSidLbl.TabIndex = 0;
            this.TwilioAccountSidLbl.Text = "Account sid:";
            // 
            // EmailGb
            // 
            this.EmailGb.Controls.Add(this.EmailPasswordTb);
            this.EmailGb.Controls.Add(this.EmailPortTb);
            this.EmailGb.Controls.Add(this.EmailSmtpServerTb);
            this.EmailGb.Controls.Add(this.EmailUseSslCb);
            this.EmailGb.Controls.Add(this.EmailLoginTb);
            this.EmailGb.Controls.Add(this.EmailPortTLbl);
            this.EmailGb.Controls.Add(this.EmailSmtpServerLbl);
            this.EmailGb.Controls.Add(this.EmailUseSslLbl);
            this.EmailGb.Controls.Add(this.EmailPasswordLbl);
            this.EmailGb.Controls.Add(this.EmailLoginLbl);
            this.EmailGb.Location = new System.Drawing.Point(12, 162);
            this.EmailGb.Name = "EmailGb";
            this.EmailGb.Size = new System.Drawing.Size(541, 217);
            this.EmailGb.TabIndex = 1;
            this.EmailGb.TabStop = false;
            this.EmailGb.Text = "EmailGb";
            // 
            // EmailPasswordTb
            // 
            this.EmailPasswordTb.Location = new System.Drawing.Point(6, 71);
            this.EmailPasswordTb.Name = "EmailPasswordTb";
            this.EmailPasswordTb.Size = new System.Drawing.Size(529, 20);
            this.EmailPasswordTb.TabIndex = 10;
            this.EmailPasswordTb.UseSystemPasswordChar = true;
            // 
            // EmailPortTb
            // 
            this.EmailPortTb.Location = new System.Drawing.Point(6, 185);
            this.EmailPortTb.Name = "EmailPortTb";
            this.EmailPortTb.Size = new System.Drawing.Size(529, 20);
            this.EmailPortTb.TabIndex = 9;
            // 
            // EmailSmtpServerTb
            // 
            this.EmailSmtpServerTb.Location = new System.Drawing.Point(6, 146);
            this.EmailSmtpServerTb.Name = "EmailSmtpServerTb";
            this.EmailSmtpServerTb.Size = new System.Drawing.Size(529, 20);
            this.EmailSmtpServerTb.TabIndex = 8;
            // 
            // EmailUseSslCb
            // 
            this.EmailUseSslCb.AutoSize = true;
            this.EmailUseSslCb.Location = new System.Drawing.Point(6, 110);
            this.EmailUseSslCb.Name = "EmailUseSslCb";
            this.EmailUseSslCb.Size = new System.Drawing.Size(68, 17);
            this.EmailUseSslCb.TabIndex = 7;
            this.EmailUseSslCb.Text = "Use SSL";
            this.EmailUseSslCb.UseVisualStyleBackColor = true;
            // 
            // EmailLoginTb
            // 
            this.EmailLoginTb.Location = new System.Drawing.Point(6, 32);
            this.EmailLoginTb.Name = "EmailLoginTb";
            this.EmailLoginTb.Size = new System.Drawing.Size(529, 20);
            this.EmailLoginTb.TabIndex = 5;
            // 
            // EmailPortTLbl
            // 
            this.EmailPortTLbl.AutoSize = true;
            this.EmailPortTLbl.Location = new System.Drawing.Point(12, 169);
            this.EmailPortTLbl.Name = "EmailPortTLbl";
            this.EmailPortTLbl.Size = new System.Drawing.Size(29, 13);
            this.EmailPortTLbl.TabIndex = 4;
            this.EmailPortTLbl.Text = "Port:";
            // 
            // EmailSmtpServerLbl
            // 
            this.EmailSmtpServerLbl.AutoSize = true;
            this.EmailSmtpServerLbl.Location = new System.Drawing.Point(12, 130);
            this.EmailSmtpServerLbl.Name = "EmailSmtpServerLbl";
            this.EmailSmtpServerLbl.Size = new System.Drawing.Size(72, 13);
            this.EmailSmtpServerLbl.TabIndex = 3;
            this.EmailSmtpServerLbl.Text = "SMTP server:";
            // 
            // EmailUseSslLbl
            // 
            this.EmailUseSslLbl.AutoSize = true;
            this.EmailUseSslLbl.Location = new System.Drawing.Point(12, 94);
            this.EmailUseSslLbl.Name = "EmailUseSslLbl";
            this.EmailUseSslLbl.Size = new System.Drawing.Size(52, 13);
            this.EmailUseSslLbl.TabIndex = 2;
            this.EmailUseSslLbl.Text = "Use SSL:";
            // 
            // EmailPasswordLbl
            // 
            this.EmailPasswordLbl.AutoSize = true;
            this.EmailPasswordLbl.Location = new System.Drawing.Point(12, 55);
            this.EmailPasswordLbl.Name = "EmailPasswordLbl";
            this.EmailPasswordLbl.Size = new System.Drawing.Size(56, 13);
            this.EmailPasswordLbl.TabIndex = 1;
            this.EmailPasswordLbl.Text = "Password:";
            // 
            // EmailLoginLbl
            // 
            this.EmailLoginLbl.AutoSize = true;
            this.EmailLoginLbl.Location = new System.Drawing.Point(12, 16);
            this.EmailLoginLbl.Name = "EmailLoginLbl";
            this.EmailLoginLbl.Size = new System.Drawing.Size(36, 13);
            this.EmailLoginLbl.TabIndex = 0;
            this.EmailLoginLbl.Text = "Login:";
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(187, 385);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(190, 34);
            this.SaveBtn.TabIndex = 3;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 425);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.EmailGb);
            this.Controls.Add(this.TwilioGb);
            this.MaximumSize = new System.Drawing.Size(581, 464);
            this.MinimumSize = new System.Drawing.Size(581, 464);
            this.Name = "Settings";
            this.Text = "Settings";
            this.TwilioGb.ResumeLayout(false);
            this.TwilioGb.PerformLayout();
            this.EmailGb.ResumeLayout(false);
            this.EmailGb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox TwilioGb;
        private System.Windows.Forms.GroupBox EmailGb;
        private System.Windows.Forms.Label TwilioPhoneNumberLbl;
        private System.Windows.Forms.Label TwilioAuthTokenLbl;
        private System.Windows.Forms.Label TwilioAccountSidLbl;
        private System.Windows.Forms.MaskedTextBox TwilioAccountSidMtb;
        private System.Windows.Forms.Label EmailPortTLbl;
        private System.Windows.Forms.Label EmailSmtpServerLbl;
        private System.Windows.Forms.Label EmailUseSslLbl;
        private System.Windows.Forms.Label EmailPasswordLbl;
        private System.Windows.Forms.Label EmailLoginLbl;
        private System.Windows.Forms.TextBox EmailLoginTb;
        private System.Windows.Forms.CheckBox EmailUseSslCb;
        private System.Windows.Forms.TextBox EmailSmtpServerTb;
        private System.Windows.Forms.TextBox EmailPortTb;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox TwilioAuthTokenTb;
        private System.Windows.Forms.MaskedTextBox TwilioPhoneNumberMtb;
        private System.Windows.Forms.TextBox EmailPasswordTb;
    }
}