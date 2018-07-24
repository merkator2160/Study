using AppointmentReminder.Models.Config;
using AppointmentReminder.Properties;
using AppointmentReminder.Services.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentReminder.Forms
{
    public partial class Settings : Form
    {
        private readonly IMessenger _messenger;
        private readonly ISettingsService _settingsService;
        private readonly RootConfig _config;


        public Settings(IMessenger messenger, ISettingsService settingsService)
        {
            InitializeComponent();

            _messenger = messenger;
            _settingsService = settingsService;
            _config = _settingsService.GetConfig();

            Text = $"{_config.AppName} - Settings";
            Icon = Resources.ResourceManager.GetObject(_config.IconName) as Icon;

            LoadSettings();
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private void SaveBtn_Click(Object sender, EventArgs e)
        {
            SaveSettings();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void LoadSettings()
        {
            TwilioAccountSidMtb.Text = _config.TwilioServiceConfig.AccountSid;
            TwilioAuthTokenTb.Text = _config.TwilioServiceConfig.AuthToken;
            TwilioPhoneNumberMtb.Text = _config.TwilioServiceConfig.TwilioNumber;

            EmailLoginTb.Text = _config.EmailServiceConfig.Login;
            EmailPasswordTb.Text = _config.EmailServiceConfig.Password;
            EmailUseSslCb.Checked = _config.EmailServiceConfig.EnabledSsl;
            EmailSmtpServerTb.Text = _config.EmailServiceConfig.SmtpUri;
            EmailPortTb.Text = _config.EmailServiceConfig.Port.ToString();
        }
        private void SaveSettings()
        {
            try
            {
                _config.TwilioServiceConfig.AccountSid = TwilioAccountSidMtb.Text;
                _config.TwilioServiceConfig.AuthToken = TwilioAuthTokenTb.Text;
                _config.TwilioServiceConfig.TwilioNumber = TwilioPhoneNumberMtb.Text;

                _config.EmailServiceConfig.Login = EmailLoginTb.Text;
                _config.EmailServiceConfig.Password = EmailPasswordTb.Text;
                _config.EmailServiceConfig.EnabledSsl = EmailUseSslCb.Checked;
                _config.EmailServiceConfig.SmtpUri = EmailSmtpServerTb.Text;
                _config.EmailServiceConfig.Port = Int32.Parse(EmailPortTb.Text);

                _settingsService.UpdateConfig(_config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
