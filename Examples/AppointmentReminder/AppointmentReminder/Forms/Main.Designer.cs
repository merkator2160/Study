namespace AppointmentReminder.Forms
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
            this.SendBtn = new System.Windows.Forms.Button();
            this.EmailOnlyCb = new System.Windows.Forms.CheckBox();
            this.MessagesRtb = new System.Windows.Forms.RichTextBox();
            this.MessageTb = new System.Windows.Forms.TextBox();
            this.InfoLbl = new System.Windows.Forms.Label();
            this.UsersLv = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(720, 390);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(115, 55);
            this.SendBtn.TabIndex = 0;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // EmailOnlyCb
            // 
            this.EmailOnlyCb.AutoSize = true;
            this.EmailOnlyCb.Checked = true;
            this.EmailOnlyCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EmailOnlyCb.Location = new System.Drawing.Point(720, 366);
            this.EmailOnlyCb.Name = "EmailOnlyCb";
            this.EmailOnlyCb.Size = new System.Drawing.Size(73, 17);
            this.EmailOnlyCb.TabIndex = 1;
            this.EmailOnlyCb.Text = "Email only";
            this.EmailOnlyCb.UseVisualStyleBackColor = true;
            // 
            // MessagesRtb
            // 
            this.MessagesRtb.Location = new System.Drawing.Point(243, 12);
            this.MessagesRtb.Name = "MessagesRtb";
            this.MessagesRtb.ReadOnly = true;
            this.MessagesRtb.Size = new System.Drawing.Size(592, 348);
            this.MessagesRtb.TabIndex = 2;
            this.MessagesRtb.Text = "";
            // 
            // MessageTb
            // 
            this.MessageTb.Location = new System.Drawing.Point(243, 390);
            this.MessageTb.Multiline = true;
            this.MessageTb.Name = "MessageTb";
            this.MessageTb.Size = new System.Drawing.Size(471, 55);
            this.MessageTb.TabIndex = 3;
            // 
            // InfoLbl
            // 
            this.InfoLbl.AutoSize = true;
            this.InfoLbl.Location = new System.Drawing.Point(243, 367);
            this.InfoLbl.Name = "InfoLbl";
            this.InfoLbl.Size = new System.Drawing.Size(35, 13);
            this.InfoLbl.TabIndex = 4;
            this.InfoLbl.Text = "label1";
            // 
            // UsersLv
            // 
            this.UsersLv.Location = new System.Drawing.Point(12, 12);
            this.UsersLv.Name = "UsersLv";
            this.UsersLv.Size = new System.Drawing.Size(225, 433);
            this.UsersLv.TabIndex = 5;
            this.UsersLv.UseCompatibleStateImageBehavior = false;
            this.UsersLv.View = System.Windows.Forms.View.List;
            this.UsersLv.SelectedIndexChanged += new System.EventHandler(this.UsersLv_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 457);
            this.Controls.Add(this.UsersLv);
            this.Controls.Add(this.InfoLbl);
            this.Controls.Add(this.MessageTb);
            this.Controls.Add(this.MessagesRtb);
            this.Controls.Add(this.EmailOnlyCb);
            this.Controls.Add(this.SendBtn);
            this.MaximumSize = new System.Drawing.Size(863, 496);
            this.MinimumSize = new System.Drawing.Size(863, 496);
            this.Name = "Main";
            this.Text = "Messenger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.CheckBox EmailOnlyCb;
        private System.Windows.Forms.RichTextBox MessagesRtb;
        private System.Windows.Forms.TextBox MessageTb;
        private System.Windows.Forms.Label InfoLbl;
        private System.Windows.Forms.ListView UsersLv;
    }
}

