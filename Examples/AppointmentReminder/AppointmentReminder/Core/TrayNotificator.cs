using AppointmentReminder.Models.BusMessages;
using AppointmentReminder.Models.Config;
using AppointmentReminder.Properties;
using AppointmentReminder.Services.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentReminder.Core
{
    internal class TrayNotificator : IDisposable, ITrayNotificator
    {
        private readonly NotifyIcon _trayIcon;
        private readonly RootConfig _config;
        private readonly IMessenger _messanger;
        private Boolean _disposed;


        public TrayNotificator(IMessenger messanger, ISettingsService settingsService)
        {
            _messanger = messanger;
            _config = settingsService.GetConfig();

            var icon = Resources.ResourceManager.GetObject(_config.IconName) as Icon;
            _trayIcon = new NotifyIcon
            {
                Icon = icon,
                Visible = true,
                BalloonTipTitle = _config.TrayConfig.BalloonTitle,
                ContextMenu = CreateMenu()
            };
            _trayIcon.DoubleClick += TrayIconOnDoubleClick;
        }


        // INotificator ///////////////////////////////////////////////////////////////////////////
        public void ShowMessage(String message)
        {
            _trayIcon.ShowBalloonTip(_config.TrayConfig.BalloonLifetime, _config.TrayConfig.BalloonTitle, message, ToolTipIcon.None);
        }
        public void ShowError(String message)
        {
            _trayIcon.ShowBalloonTip(_config.TrayConfig.BalloonLifetime, _config.TrayConfig.BalloonTitle, message, ToolTipIcon.Error);
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private ContextMenu CreateMenu()
        {
            var menu = new ContextMenu();

            var settingsMenuItem = new MenuItem("Settings");
            settingsMenuItem.Click += SettingsMenuItemOnClick;
            menu.MenuItems.Add(settingsMenuItem);

            var exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += ExitMenuOnClick;
            menu.MenuItems.Add(exitMenuItem);

            return menu;
        }


        // EVENTS /////////////////////////////////////////////////////////////////////////////////
        private void TrayIconOnDoubleClick(Object sender, EventArgs eventArgs)
        {
            _messanger.Send(new TrayIconClickMessage());
        }
        private void ExitMenuOnClick(Object sender, EventArgs eventArgs)
        {
            Application.Exit();
        }
        private void SettingsMenuItemOnClick(Object sender, EventArgs eventArgs)
        {
            _messanger.Send(new SettingsTrayMenuItemClickMessage());
        }


        // IDisposable ////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }
        }
    }
}