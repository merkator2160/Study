﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CodeForces.UsefulItems.Updater
{
	public class Startup
	{
		public void Run(String[] args)
		{
			try
			{
				var process = args[1].Replace(".exe", "");

				Console.WriteLine("Terminate process!");
				while(Process.GetProcessesByName(process).Length > 0)
				{
					var myProcesses2 = Process.GetProcessesByName(process);
					for(var i = 1; i < myProcesses2.Length; i++)
					{
						myProcesses2[i].Kill();
					}

					Thread.Sleep(300);
				}
				if(File.Exists(args[1]))
				{
					File.Delete(args[1]);
				}

				File.Move(args[1], args[0]);

				Console.WriteLine("Starting: " + args[1]);
				Process.Start(args[1]);
			}
			catch(Exception)
			{

			}
		}
	}
}