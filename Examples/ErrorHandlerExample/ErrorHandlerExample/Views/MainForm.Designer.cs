namespace ErrorHandlerExample.Views
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
            this.generateExceptionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generateExceptionButton
            // 
            this.generateExceptionButton.Location = new System.Drawing.Point(45, 71);
            this.generateExceptionButton.Name = "generateExceptionButton";
            this.generateExceptionButton.Size = new System.Drawing.Size(191, 69);
            this.generateExceptionButton.TabIndex = 0;
            this.generateExceptionButton.Text = "Generate Exception";
            this.generateExceptionButton.UseVisualStyleBackColor = true;
            this.generateExceptionButton.Click += new System.EventHandler(this.generateExceptionButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.generateExceptionButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button generateExceptionButton;
    }
}

