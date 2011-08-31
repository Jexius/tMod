using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Events
{
	public delegate void AvatarPacketReceivedEventHandler(object sender, AvatarPacketReceivedEventArgs e);

	public class AvatarPacketReceivedEventArgs : PacketReceivedEventArgs
	{
		public string PlayerName { get; private set; }

		internal AvatarPacketReceivedEventArgs(PacketReceivedEventArgs e)
			: base(e)
		{
			int index = Start +	28;
			PlayerName = Encoding.ASCII.GetString(ReadBuffer, index, (Length - index) + Start);
		}
	}
}
