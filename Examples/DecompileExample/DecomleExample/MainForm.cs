using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Common;

namespace DecomleExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StackFrame stackFrame;
            string stackTrace = StackTraceHelper.Get(out stackFrame);
            SetTextBoxesValues(stackTrace, stackFrame);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StackFrame stackFrame;
            string stackTrace = StackTraceHelper.Get(out stackFrame);
            SetTextBoxesValues(stackTrace, stackFrame);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StackFrame stackFrame;
            string stackTrace = StackTraceHelper.Get(out stackFrame);
            SetTextBoxesValues(stackTrace, stackFrame);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StackFrame stackFrame;
            string stackTrace = StackTraceHelper.Get(out stackFrame);
            SetTextBoxesValues(stackTrace, stackFrame);
        }

        private void SetTextBoxesValues(string stackTrace, StackFrame stackFrame)
        {
            stackTraceTextBox.Text = stackTrace;
            methodBodyTextBox.Text = GetMethodBody(stackFrame);
        }

        private static string GetMethodBody(StackFrame stackFrame)
        {
            MethodBase methodBase = stackFrame.GetMethod();
            return Decompiler.GetSourceCode(methodBase.Module.FullyQualifiedName, methodBase.DeclaringType.Name, methodBase.Name);
        }
    }
}
