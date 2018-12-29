using GalaSoft.MvvmLight.Messaging;
using Hardcodet.Wpf.TaskbarNotification;
using HhScraper.Models.BusMessages;
using HhScraper.Models.Config;
using HhScraper.Properties;
using HhScraper.Services.Interfaces;
using HhScraper.Views;
using Microsoft.Practices.Unity;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace HhScraper.Core
{
    internal class TrayNotificator : IDisposable, ITrayNotificator
    {
        private readonly TaskbarIcon _trayIcon;
        private readonly RootConfig _config;
        private readonly IMessenger _messanger;
        private readonly IUnityContainer _container;
        private Boolean _disposed;


        public TrayNotificator(IMessenger messanger, ISettingsService settingsService, IUnityContainer container)
        {
            _messanger = messanger;
            _container = container;
            _config = settingsService.GetConfig();

            var icon = Resources.ResourceManager.GetObject(_config.IconName) as Icon;
            _trayIcon = new TaskbarIcon
            {
                Icon = icon,
                Visibility = Visibility.Visible,
                ToolTipText = _config.TrayConfig.BalloonTitle,
                TrayPopup = _container.Resolve<FancyPopup>()
            };
            _trayIcon.TrayMouseDoubleClick += TrayIconOnTrayMouseDoubleClick;
        }


        // INotificator ///////////////////////////////////////////////////////////////////////////
        public void ShowMessage(String message)
        {
            _trayIcon.ShowBalloonTip(_config.TrayConfig.BalloonTitle, message, BalloonIcon.None);
        }
        public void ShowError(String message)
        {
            _trayIcon.ShowBalloonTip(_config.TrayConfig.BalloonTitle, message, BalloonIcon.Error);
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
        private void TrayIconOnTrayMouseDoubleClick(Object sender, RoutedEventArgs routedEventArgs)
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

                //_trayIcon.Visibility = Visibility.Hidden;
                _trayIcon.Dispose();
            }
        }
    }
}