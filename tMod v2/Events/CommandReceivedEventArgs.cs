using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Events
{
	public delegate void CommandReceivedEventHandler(object sender, CommandReceivedEventArgs e);

	public class CommandReceivedEventArgs : CancelableEventArgs
	{
		public int Player { get; private set; }
		public string Text { get; private set; }

		internal CommandReceivedEventArgs(int player, string text)
		{
			Player = player;
			Text = text;
		}
	}
}
