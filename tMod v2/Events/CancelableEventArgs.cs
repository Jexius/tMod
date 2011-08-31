using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Events
{
	public delegate void CancelableEventHandler(object sender, CancelableEventArgs e);

	public class CancelableEventArgs : EventArgs
	{
		public bool Canceled { get; private set; }

		public virtual void Cancel()
		{
			Canceled = true;
		}
	}
}
