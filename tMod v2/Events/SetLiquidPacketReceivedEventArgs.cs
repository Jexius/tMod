using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;

namespace tMod_v3.Events
{
	public delegate void SetLiquidPacketReceivedEventHandler(object sender, SetLiquidPacketReceivedEventArgs e);
	
	public class SetLiquidPacketReceivedEventArgs : PacketReceivedEventArgs
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public byte LiquidAmount { get; private set; }
		public bool Lava { get; private set; }

		internal SetLiquidPacketReceivedEventArgs(PacketReceivedEventArgs e)
			: base(e)
		{
			int index = 1 + Start;
			X = BitConverter.ToInt32(ReadBuffer, index);
			index += 4;
			Y = BitConverter.ToInt32(ReadBuffer, index);
			index += 4;
			LiquidAmount = ReadBuffer[index];
			index++;
			Lava = ReadBuffer[index] == 1;
		}

		public override void Cancel()
		{
			NetMessageMod.SendData(0x30, Player, -1, "", X, Y, 0f, 0f);
			base.Cancel();
		}
	}
}
