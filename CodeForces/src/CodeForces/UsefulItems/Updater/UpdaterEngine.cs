using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace CodeForces.UsefulItems.Updater
{
	public class UpdaterEngine : IDisposable
	{
		private Boolean _disposed;
		private readonly UpdatingEngineConfig _config;
		private readonly String _productUrl;
		private readonly String _currentApplicationVersion;

		private WebClient _webClient;


		public UpdaterEngine(String productUrl, String currentApplicationVersion, UpdatingEngineConfig config)
		{
			_config = config;
			_productUrl = productUrl;
			_currentApplicationVersion = currentApplicationVersion;

			_webClient = new WebClient();
			_webClient.DownloadProgressChanged += OnDownloadProgressChanged;
			_webClient.DownloadFileCompleted += OnDownloadCompleted;
		}
		~UpdaterEngine()
		{
			Dispose(false);
		}


		// EVENTS /////////////////////////////////////////////////////////////////////////////////
		public delegate void UpdateProgressChangedEventHandler(Object sender, UpdateProgressChangedEventArgs e);
		public event UpdateProgressChangedEventHandler UpdateProgressChanged = (sender, args) => { };


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		public void CheckUpdates()
		{
			String remoteVersionStr;
			if(!TryGetRemoteVersion(out remoteVersionStr))
				return;

			if(File.Exists(_config.SetupFileName))
			{
				var localSetupFileVersion = new Version(FileVersionInfo.GetVersionInfo(_config.SetupFileName).FileVersion);
				var currentApplicationVersion = new Version(_currentApplicationVersion);

				if(localSetupFileVersion == currentApplicationVersion)
				{

				}
			}


			try
			{
				if(File.Exists(_config.SetupFileName) && new Version(FileVersionInfo.GetVersionInfo(_config.SetupFileName).FileVersion) > new Version(_currentApplicationVersion))
				{
					Process.Start(_config.UpdaterFileName, $"{_config.SetupFileName} \"{Process.GetCurrentProcess().ProcessName}\"");
					Process.GetCurrentProcess().CloseMainWindow();
				}
				else
				{
					if(File.Exists(_config.SetupFileName))
					{
						File.Delete(_config.SetupFileName);
					}
					DownloadSetupFile();
				}
			}
			catch(Exception)
			{
				if(File.Exists(_config.SetupFileName))
				{
					File.Delete(_config.SetupFileName);
				}
				DownloadSetupFile();
			}
		}
		private Boolean TryGetRemoteVersion(out String remoteVersion)
		{
			try
			{
				remoteVersion = _webClient.DownloadString(Path.Combine(_productUrl, _config.VersionFileName));
				return true;
			}
			catch(Exception)
			{
				remoteVersion = String.Empty;
				return false;
			}
		}
		private void DownloadSetupFile()
		{
			var remoteVersionStr = _webClient.DownloadString(Path.Combine(_productUrl, _config.VersionFileName));

			var remoteVersion = new Version(remoteVersionStr);
			var localVersion = new Version(_currentApplicationVersion);

			if(localVersion < remoteVersion)
			{
				if(File.Exists(_config.SetupFileName))
				{
					File.Delete(_config.SetupFileName);
				}

				_webClient.DownloadFileAsync(new Uri(Path.Combine(_productUrl, _config.SetupFileName)), _config.SetupTempFileName);
			}
		}


		// EVENT HANDLERS /////////////////////////////////////////////////////////////////////////
		private void OnDownloadCompleted(Object sender, AsyncCompletedEventArgs e)
		{
			Process.Start(_config.UpdaterFileName, $"{_config.SetupTempFileName} {_config.SetupFileName}");
			Process.GetCurrentProcess().Kill();
		}
		private void OnDownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
		{
			UpdateProgressChanged.Invoke(this, new UpdateProgressChangedEventArgs()
			{
				Message = "Downlouding updates...",
				BytesReceived = e.BytesReceived,
				TotalBytesToReceive = e.TotalBytesToReceive,
				ProgressPercentage = e.ProgressPercentage
			});
		}


		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(Boolean disposing)
		{
			if(!_disposed)
			{
				ReleaseUnmanagedResources();
				if(disposing)
					ReleaseManagedResources();

				_disposed = true;
			}
		}
		private void ReleaseUnmanagedResources()
		{
			// We didn't have its yet.
		}
		private void ReleaseManagedResources()
		{
			_webClient?.Dispose();
		}
	}
}