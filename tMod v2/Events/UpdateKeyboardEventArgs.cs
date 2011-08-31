using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace tMod_v3.Events
{
	public delegate void UpdateKeyboardEventHandler(object sender, UpdateKeyboardEventArgs e);

	public class UpdateKeyboardEventArgs : EventArgs
	{
		public KeyboardState CurrentState { get; private set; }
		public KeyboardState PreviousState { get; private set; }

		public UpdateKeyboardEventArgs(KeyboardState currentState, KeyboardState previousState)
		{
			CurrentState = currentState;
			PreviousState = previousState;
		}
	}
}
