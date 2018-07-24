using AppointmentReminder.Models;
using AppointmentReminder.Models.Config;
using AppointmentReminder.Models.ViewModels;
using AppointmentReminder.Properties;
using AppointmentReminder.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentReminder.Forms
{
    public partial class Main : Form
    {
        private readonly ITwilioService _twilioService;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly RootConfig _config;


        public Main(ISettingsService configService, ITwilioService twilioService, IEmailService emailService, ILogger logger)
        {
            InitializeComponent();

            _twilioService = twilioService;
            _emailService = emailService;
            _logger = logger;
            _config = configService.GetConfig();

            Text = $"{_config.AppName} - Messanger";
            InfoLbl.Text = "";
            Icon = Resources.ResourceManager.GetObject(_config.IconName) as Icon;

            FillUserList(new[]
            {
                new UserTag()
                {
                    Id = 1,
                    UserName = "Test user",
                    Email = "info@eecsl.com",    // "info@eecsl.com"
                    Phone = "+14082054574"      //"+79875341715"      // +14082054574
                }
            });
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private async void SendBtn_Click(Object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(MessageTb.Text))
                return;

            if (UsersLv.SelectedItems.Count == 0)
                return;

            try
            {
                var selectedUserData = UsersLv.SelectedItems[0].Tag as UserTag;
                if (!EmailOnlyCb.Checked)
                {
                    await _twilioService.SendSmsMessageAsync(selectedUserData.Phone, MessageTb.Text);
                }
                await _emailService.SendAsync(new EmailMessage()
                {
                    Subject = "Appointment reminder notification",
                    Body = MessageTb.Text,
                    Destination = selectedUserData.Email
                });

                MessagesRtb.Text += $"Sent: {MessageTb.Text}\r\n";
                MessageTb.Clear();
            }
            catch (Exception ex)
            {
                MessagesRtb.Text += $"Error: {ex.Message}\r\n";
                _logger.Error(ex.Message);
            }
        }
        private void UsersLv_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (UsersLv.SelectedItems.Count == 0)
                return;

            var selectedUserData = UsersLv.SelectedItems[0].Tag as UserTag;
            InfoLbl.Text = $"Phone: {selectedUserData.Phone}    Email: {selectedUserData.Email}";
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void FillUserList(IReadOnlyCollection<UserTag> userData)
        {
            foreach (var x in userData)
            {
                UsersLv.Items.Add(new ListViewItem()
                {
                    Text = x.UserName,
                    Tag = x
                });
            }

            if (UsersLv.Items.Count != 0)
                UsersLv.Items[0].Selected = true;
        }
    }
}