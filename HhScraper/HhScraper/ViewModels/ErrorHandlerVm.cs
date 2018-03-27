using HhScraper.Models;
using HhScraper.Models.Config;
using HhScraper.Services.Interfaces;
using HhScraper.Views;
using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace HhScraper.ViewModels
{
    public class ErrorHandlerVm
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailSevice;
        private readonly RootConfig _config;
        private readonly ErrorHandlerForm _view;
        private Boolean _isDetailShow;
        private Exception _exception;


        public ErrorHandlerVm(ILogger logger, ISettingsService settingsService, IUnityContainer container, IEmailService emailSevice)
        {
            _logger = logger;
            _emailSevice = emailSevice;
            _config = settingsService.GetConfig();

            _view = container.Resolve<ErrorHandlerForm>();
            _view.OnSendButtonClick += View_OnSendButtonClick;
            _view.OnShowErrorLinkClick += View_OnShowErrorLink;
            _view.OnLogFileLinkClick += View_OnLogFileLinkClick;
            _view.OnDebugButtonClick += View_OnDebugButtonClick;
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private void View_OnSendButtonClick()
        {
            try
            {
                var messageBody = $"Additional information: {_view.ExceptionDetailText} \n\n " +
                                  $"Reply to: {_view.ReplyEmail} \n\n " +
                                  $"Error: {_view.ExceptionInfoText}";

                _emailSevice.Send(new EmailMessage()
                {
                    Subject = $"{_config.AppName} exception!",
                    Body = messageBody,
                    Destination = _config.ProgrammerEmail
                });
                MessageBox.Show("Email sended!", "Email..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                MessageBox.Show(
                    "Error, оr impossible to send report to the developer. \n " +
                    "Please send the log file to the developer's email. " + _config.ProgrammerEmail + "\n" +
                    "Log File:" + Environment.CurrentDirectory + " \n\n" + ex);

                if (MessageBox.Show("Attach debugger? \n Only for developer!!!", "Debugging...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Debugger.Launch();
                    throw;
                }
            }
        }
        private void View_OnShowErrorLink()
        {
            _isDetailShow = !_isDetailShow;
            ShowDetailInfoException(_isDetailShow);
        }
        private void View_OnDebugButtonClick()
        {
            if (MessageBox.Show("Attach debugger? \n Only for developer!!!", "Debugging...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Debugger.Launch();
            }
        }
        private void View_OnLogFileLinkClick()
        {
            Process.Start(Environment.CurrentDirectory + "\\log-file.txt");
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void ShowDetailInfoException(Boolean isShow)
        {
            const Int32 minimazedHeight = 256;
            const Int32 maximazedHeight = 515;

            _view.Height = isShow ? maximazedHeight : minimazedHeight;
            _view.ShowExceptionInfoTextBox(isShow);
        }
        public void Run(Exception exception)
        {
            _exception = exception;
            ShowDetailInfoException(_isDetailShow);

            var exceptionInfoText = String.Format(
                "An unexpected error occurred: {0}" + Environment.NewLine +
                "Time: {1} " + Environment.NewLine +
                "{2}" + Environment.NewLine +
                "InnerException: \n {3}" + Environment.NewLine +
                "InnerException StackTrace: \n  {4}" + Environment.NewLine,
                _exception.Message,
                DateTime.Now,
                _exception,
                _exception.InnerException,
                _exception.InnerException != null ? _exception.InnerException.StackTrace : String.Empty
            );

            _logger.Error(exceptionInfoText);
            _view.ExceptionInfoText = exceptionInfoText;
            _view.ShowDialog();
        }
    }
}
