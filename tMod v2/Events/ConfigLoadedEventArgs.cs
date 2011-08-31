using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Events
{
	public delegate void ConfigLoadedEventHandler(object sender, ConfigLoadedEventArgs e);

	public class ConfigLoadedEventArgs : EventArgs
	{
		public string File { get; private set; }

		public ConfigLoadedEventArgs(string file)
		{
			File = file;
		}
	}
}
