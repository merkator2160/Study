using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CodeForces.UsefulItems
{
	public static class VersionExtractor
	{
		public static String Extract(String extractionTargetFileName)
		{
			var currentDirectory = Directory.GetCurrentDirectory();
			var files = Directory.GetFiles(currentDirectory);
			var firstExecutable = files.FirstOrDefault(p => p.Contains(extractionTargetFileName));

			if(firstExecutable == null)
				throw new FileNotFoundException($"File: {extractionTargetFileName} was not found!");

			return FileVersionInfo.GetVersionInfo(firstExecutable).FileVersion;
		}
	}
}