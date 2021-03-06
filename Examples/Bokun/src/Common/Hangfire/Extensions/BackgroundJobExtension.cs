﻿using Common.Hangfire.Interfaces;
using Hangfire;

namespace Common.Hangfire.Extensions
{
	public static class BackgroundJobExtension
	{
		public static void Enqueue<T>(this BackgroundJob job) where T : IJob
		{
			BackgroundJob.Enqueue<T>(p => p.ExecuteAsync());
		}
	}
}