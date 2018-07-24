using AppointmentReminder.Forms;
using AppointmentReminder.Models.BusMessages;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.Unity;

namespace AppointmentReminder.Core
{
    public class FormsManager
    {
        private readonly IUnityContainer _container;
        private readonly IMessenger _messenger;

        private Main _mainForm;
        private Settings _settingsForm;


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
            if (_mainForm == null || _mainForm.IsDisposed)
            {
                _mainForm = _container.Resolve<Main>();
            }

            if (!_mainForm.Visible)
            {
                _mainForm.ShowDialog();
            }
        }
        public void ShowSettings()
        {
            if (_settingsForm == null || _settingsForm.IsDisposed)
            {
                _settingsForm = _container.Resolve<Settings>();
            }

            if (!_settingsForm.Visible)
            {
                _settingsForm.ShowDialog();
            }
        }
    }
}