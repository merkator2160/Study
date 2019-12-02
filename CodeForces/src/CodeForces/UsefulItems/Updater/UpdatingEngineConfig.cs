using System;

namespace CodeForces.UsefulItems.Updater
{
	public class UpdatingEngineConfig
	{
		private const String DefaultUpdaterFileName = "updater.exe";
		private const String DefaultVersionFileName = "version.txt";
		private const String DefaultSetupFileName = "setup.exe";
		private const String DefaultSetupTempFileName = "setupTemp.bin";


		public UpdatingEngineConfig()
		{
			UpdaterFileName = DefaultUpdaterFileName;
			VersionFileName = DefaultVersionFileName;
			SetupFileName = DefaultSetupFileName;
			SetupTempFileName = DefaultSetupTempFileName;
		}


		// PROPERTIES /////////////////////////////////////////////////////////////////////////////
		public String UpdaterFileName { get; set; }
		public String VersionFileName { get; set; }
		public String SetupFileName { get; set; }
		public String SetupTempFileName { get; set; }
	}
}