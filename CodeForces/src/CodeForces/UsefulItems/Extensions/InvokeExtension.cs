﻿using System;
using System.Windows.Forms;

namespace CodeForces.UsefulItems.Extensions
{
	public static class InvokeExtension
	{
		public static void InvokeEx(this Control control, Action action)
		{
			if(control.InvokeRequired)
			{
				control.Invoke(action);
			}
			else
			{
				action();
			}
		}
		public static void InvokeEx<T>(this Control control, Action<T> action, params T[] arg)
		{
			if(control.InvokeRequired)
			{
				control.Invoke(action, arg);
			}
			else
			{
				action.DynamicInvoke(arg);
			}
		}
	}
}