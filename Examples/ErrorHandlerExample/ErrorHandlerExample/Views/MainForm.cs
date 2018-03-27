using System;
using System.Windows.Forms;

namespace ErrorHandlerExample.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void generateExceptionButton_Click(object sender, System.EventArgs e)
        {
            throw new Exception("new exception");
        }
    }
}
