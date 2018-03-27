using GalaSoft.MvvmLight.Messaging;
using HhScraper.Models.BusMessages;
using HhScraper.Views;
using Microsoft.Practices.Unity;

namespace HhScraper.Core
{
    public class FormsManager
    {
        private readonly IUnityContainer _container;
        private readonly IMessenger _messenger;

        private Scraper _mainWindow;
        private Settings _settingsWindow;


        public FormsManager(IUnityContainer container, IMessenger messenger)
        {
            _container = container;
            _messenger = messenger;

            messenger.Register<TrayIconClickMessage>(this, OnTrayIconDoubleClick);
            messenger.Register<SettingsTrayMenuItemClickMessage>(this, OnSettingsTrayMenuItemClick);
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private void OnTrayIconDoubleClick(TrayIconClickMessage obj)
        {
            ShowMain();
        }
        private void OnSettingsTrayMenuItemClick(SettingsTrayMenuItemClickMessage obj)
        {
            ShowSettings();
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void ShowMain()
        {
            if (_mainWindow == null)
            {
                _mainWindow = _container.Resolve<Scraper>();
            }

            if (!_mainWindow.IsLoaded)
            {
                _mainWindow.ShowDialog();
            }
        }
        public void ShowSettings()
        {
            if (_mainWindow == null)
            {
                _settingsWindow = _container.Resolve<Settings>();
            }

            if (!_settingsWindow.IsLoaded)
            {
                _settingsWindow.ShowDialog();
            }
        }
    }
}