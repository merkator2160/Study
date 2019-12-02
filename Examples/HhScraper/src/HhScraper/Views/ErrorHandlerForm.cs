using System;
using System.Windows.Forms;

namespace HhScraper.Views
{
    public partial class ErrorHandlerForm : Form
    {
        public Action OnSendButtonClick;
        public Action OnShowErrorLinkClick;
        public Action OnLogFileLinkClick;
        public Action OnDebugButtonClick;



        public ErrorHandlerForm()
        {
            InitializeComponent();
            SubscribeEvents();
        }



        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public String ExceptionInfoText
        {
            get { return exceptionInfoTextBox.Text; }
            set { exceptionInfoTextBox.Text = value; }
        }
        public String ExceptionDetailText
        {
            get { return detailInfoTextBox.Text; }
            set { detailInfoTextBox.Text = value; }
        }
        public void ShowExceptionInfoTextBox(Boolean isShow)
        {
            exceptionInfoTextBox.Visible = isShow;
        }
        public String ReplyEmail
        {
            get { return replyEmailTextBox.Text; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////

        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void SubscribeEvents()
        {
            sendButton.Click += (sender, e) => OnSendButtonClick();
            debugButton.Click += (sender, e) => OnDebugButtonClick();
            showErrorLink.Click += (sender, e) => OnShowErrorLinkClick();
            logFileLink.Click += (sender, e) => OnLogFileLinkClick();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////


    }
}
