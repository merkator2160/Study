namespace EchoServerGraphical
{
    partial class Server
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ServerRUNbtn = new System.Windows.Forms.Button();
            this.ServerStatuslbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipTXT = new System.Windows.Forms.TextBox();
            this.portTXT = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Location = new System.Drawing.Point(-3, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(207, 262);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // ServerRUNbtn
            // 
            this.ServerRUNbtn.Location = new System.Drawing.Point(210, 227);
            this.ServerRUNbtn.Name = "ServerRUNbtn";
            this.ServerRUNbtn.Size = new System.Drawing.Size(100, 23);
            this.ServerRUNbtn.TabIndex = 1;
            this.ServerRUNbtn.Text = "ON";
            this.ServerRUNbtn.UseVisualStyleBackColor = true;
            this.ServerRUNbtn.Click += new System.EventHandler(this.ServerRUNbtn_Click);
            // 
            // ServerStatuslbl
            // 
            this.ServerStatuslbl.AutoSize = true;
            this.ServerStatuslbl.Location = new System.Drawing.Point(213, 211);
            this.ServerStatuslbl.Name = "ServerStatuslbl";
            this.ServerStatuslbl.Size = new System.Drawing.Size(37, 13);
            this.ServerStatuslbl.TabIndex = 2;
            this.ServerStatuslbl.Text = "Offline";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // ipTXT
            // 
            this.ipTXT.Location = new System.Drawing.Point(210, 19);
            this.ipTXT.Name = "ipTXT";
            this.ipTXT.Size = new System.Drawing.Size(100, 20);
            this.ipTXT.TabIndex = 5;
            this.ipTXT.Text = "127.0.0.1";
            // 
            // portTXT
            // 
            this.portTXT.Location = new System.Drawing.Point(210, 58);
            this.portTXT.Name = "portTXT";
            this.portTXT.Size = new System.Drawing.Size(100, 20);
            this.portTXT.TabIndex = 6;
            this.portTXT.Text = "1116";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 262);
            this.Controls.Add(this.portTXT);
            this.Controls.Add(this.ipTXT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerStatuslbl);
            this.Controls.Add(this.ServerRUNbtn);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Server_FormClosed);
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button ServerRUNbtn;
        private System.Windows.Forms.Label ServerStatuslbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipTXT;
        private System.Windows.Forms.TextBox portTXT;
    }
}

