using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3.Events
{
	public delegate void PacketReceivedEventHandler(object sender, PacketReceivedEventArgs e);

	public class PacketReceivedEventArgs : CancelableEventArgs
	{
		public PacketTypes PacketType { get; private set; }
		public int Start { get; private set; }
		public int Length { get; private set; }
		public byte[] ReadBuffer { get; private set; }
		public byte Player { get; private set; }
		internal dynamic Instance { get; private set; }

		internal PacketReceivedEventArgs(PacketReceivedEventArgs e)
		{
			Instance = e.Instance;
			PacketType = e.PacketType;
			Start = e.Start;
			Length = e.Length;
			ReadBuffer = e.ReadBuffer;
			Player = e.Player;
		}

		internal PacketReceivedEventArgs(dynamic inst, int start, int length)
		{
			Instance = inst;
			PacketType = (PacketTypes)inst.readBuffer[start];
			Start = start;
			Length = length;
			ReadBuffer = inst.readBuffer;
			Player = (byte)(int)inst.whoAmI;
		}
	}
}
