using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EPPlusExcelFormDemo
{
    public partial class FrmFunctions : Form
    {
        public FrmFunctions()
        {
            InitializeComponent();
        }
        public FrmFunctions(List<String> functions)
        {
            InitializeComponent();

            var sb = new StringBuilder();
            foreach (var f in functions)
            {
                sb.AppendLine(f.ToUpper());
            }
            textBox1.Text = sb.ToString();
        }
    }
}
