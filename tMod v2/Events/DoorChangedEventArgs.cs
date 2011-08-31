using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Events
{
	public delegate void DoorChangedEventHandler(object sender, DoorChangedEventArgs e);

	public class DoorChangedEventArgs : CancelableEventArgs
	{
		public byte PlayerId { get; private set; }
		public bool Action { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }

		public DoorChangedEventArgs(byte playerId, bool action, int x, int y)
		{
			PlayerId = playerId;
			Action = action;
			X = x;
			Y = y;
		}
	}
}
