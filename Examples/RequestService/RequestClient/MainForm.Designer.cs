namespace RequestClient
{
    partial class MainForm
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
            this.addUserBtn = new System.Windows.Forms.Button();
            this.firstNameTb = new System.Windows.Forms.TextBox();
            this.lastNameTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLastOperationStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.requestTypeCb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userIdTb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.messageTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addRequestBtn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addUserBtn
            // 
            this.addUserBtn.Location = new System.Drawing.Point(12, 90);
            this.addUserBtn.Name = "addUserBtn";
            this.addUserBtn.Size = new System.Drawing.Size(75, 23);
            this.addUserBtn.TabIndex = 0;
            this.addUserBtn.Text = "Add user";
            this.addUserBtn.UseVisualStyleBackColor = true;
            this.addUserBtn.Click += new System.EventHandler(this.addUserBtn_Click);
            // 
            // firstNameTb
            // 
            this.firstNameTb.Location = new System.Drawing.Point(12, 25);
            this.firstNameTb.Name = "firstNameTb";
            this.firstNameTb.Size = new System.Drawing.Size(100, 20);
            this.firstNameTb.TabIndex = 1;
            // 
            // lastNameTb
            // 
            this.lastNameTb.Location = new System.Drawing.Point(12, 64);
            this.lastNameTb.Name = "lastNameTb";
            this.lastNameTb.Size = new System.Drawing.Size(100, 20);
            this.lastNameTb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Last name:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLbl,
            this.toolStripStatusLabel1,
            this.toolStripLastOperationStatusLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 290);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(495, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLbl
            // 
            this.toolStripStatusLbl.Name = "toolStripStatusLbl";
            this.toolStripStatusLbl.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLbl.Text = "Disconnected";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "    Message:";
            // 
            // toolStripLastOperationStatusLbl
            // 
            this.toolStripLastOperationStatusLbl.Name = "toolStripLastOperationStatusLbl";
            this.toolStripLastOperationStatusLbl.Size = new System.Drawing.Size(34, 17);
            this.toolStripLastOperationStatusLbl.Text = "none";
            // 
            // requestTypeCb
            // 
            this.requestTypeCb.FormattingEnabled = true;
            this.requestTypeCb.Location = new System.Drawing.Point(147, 25);
            this.requestTypeCb.Name = "requestTypeCb";
            this.requestTypeCb.Size = new System.Drawing.Size(121, 21);
            this.requestTypeCb.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(144, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Request type:";
            // 
            // userIdTb
            // 
            this.userIdTb.Location = new System.Drawing.Point(147, 65);
            this.userIdTb.Name = "userIdTb";
            this.userIdTb.Size = new System.Drawing.Size(213, 20);
            this.userIdTb.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "User ID:";
            // 
            // messageTb
            // 
            this.messageTb.Location = new System.Drawing.Point(147, 108);
            this.messageTb.Multiline = true;
            this.messageTb.Name = "messageTb";
            this.messageTb.Size = new System.Drawing.Size(324, 162);
            this.messageTb.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Message:";
            // 
            // addRequestBtn
            // 
            this.addRequestBtn.Location = new System.Drawing.Point(366, 63);
            this.addRequestBtn.Name = "addRequestBtn";
            this.addRequestBtn.Size = new System.Drawing.Size(75, 23);
            this.addRequestBtn.TabIndex = 11;
            this.addRequestBtn.Text = "Add request";
            this.addRequestBtn.UseVisualStyleBackColor = true;
            this.addRequestBtn.Click += new System.EventHandler(this.addRequestBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 312);
            this.Controls.Add(this.addRequestBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.messageTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.userIdTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.requestTypeCb);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lastNameTb);
            this.Controls.Add(this.firstNameTb);
            this.Controls.Add(this.addUserBtn);
            this.Name = "MainForm";
            this.Text = "Request form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addUserBtn;
        private System.Windows.Forms.TextBox firstNameTb;
        private System.Windows.Forms.TextBox lastNameTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLastOperationStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox requestTypeCb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userIdTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox messageTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addRequestBtn;
    }
}

