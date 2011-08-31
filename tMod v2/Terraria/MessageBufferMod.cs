using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.IO;
using Microsoft.Xna.Framework;
using tMod_v3;
using tMod_v3.Events;

namespace Terraria
{
    public class MessageBufferMod
    {
        public static Type messageBuffer;
        private static DateTime[] LastDestroy = new DateTime[0xff];
        private static int[] Destroys = new int[0xff];
        private static DateTime[] LastCreate = new DateTime[0xff];
        private static int[] Creates = new int[0xff];

        public static event TileChangedEventHandler TileChanged;
        public static event DoorChangedEventHandler DoorChanged;
        public static event SetLiquidEventHandler SetLiquid;

        public static event PacketReceivedEventHandler PacketReceived;
        public static event PacketReceivedEventHandler VersionReceived;
        public static event PacketReceivedEventHandler DisconnectReceived;
        public static event PacketReceivedEventHandler ConnectedReceived;
        public static event PacketReceivedEventHandler AvatarReceived;
        public static event PacketReceivedEventHandler InventorySlotReceived;
        public static event PacketReceivedEventHandler ReadyReceived;
        public static event PacketReceivedEventHandler WorldParamsReceived;
        public static event PacketReceivedEventHandler RequestChunkReceived;
        public static event PacketReceivedEventHandler StatusReceived;
        public static event PacketReceivedEventHandler SendRowReceived;
        public static event PacketReceivedEventHandler CalculateFrameReceived;
        public static event PacketReceivedEventHandler SpawnPlayerReceived;
        public static event PacketReceivedEventHandler UpdatePlayerReceived;
        public static event PacketReceivedEventHandler IsPlayerActiveReceived;
        public static event PacketReceivedEventHandler RequestPlayerSyncReceived;
        public static event PacketReceivedEventHandler PlayerLifeReceived;
        public static event PacketReceivedEventHandler ChangeTileReceived;
        public static event PacketReceivedEventHandler UpdateTimeReceived;
        public static event PacketReceivedEventHandler DoorChangeReceived;
        public static event PacketReceivedEventHandler UpdateTilesReceived;
        public static event PacketReceivedEventHandler DropItemReceived;
        public static event PacketReceivedEventHandler ItemOwnerReceived;
        public static event PacketReceivedEventHandler NpcDataReceived;
        public static event PacketReceivedEventHandler PlayerHitNpcReceived;
        public static event PacketReceivedEventHandler ChatMessageReceived;
        public static event PacketReceivedEventHandler PlayerStrikeReceived;
        public static event PacketReceivedEventHandler ProjectileReceived;
        public static event PacketReceivedEventHandler StrikeNpcReceived;
        public static event PacketReceivedEventHandler DisposeProjectileReceived;
        public static event PacketReceivedEventHandler SetPvpReceived;
        public static event PacketReceivedEventHandler RequestChestReceived;
        public static event PacketReceivedEventHandler ChestItemReceived;
        public static event PacketReceivedEventHandler OpenChestReceived;
        public static event PacketReceivedEventHandler KillTileReceived;
        public static event PacketReceivedEventHandler HealEffectReceived;
        public static event PacketReceivedEventHandler SetZoneReceived;
        public static event PacketReceivedEventHandler RequestPasswordReceived;
        public static event PacketReceivedEventHandler SendPasswordReceived;
        public static event PacketReceivedEventHandler RemoveItemOwnerReceived;
        public static event PacketReceivedEventHandler TalkToNpcReceived;
        public static event PacketReceivedEventHandler PlayerAnimationReceived;
        public static event PacketReceivedEventHandler PlayerManaReceived;
        public static event PacketReceivedEventHandler ManaEffectReceived;
        public static event PacketReceivedEventHandler PlayerDeathReceived;
        public static event PacketReceivedEventHandler ChangeTeamsReceived;
        public static event PacketReceivedEventHandler RequestSignReceived;
        public static event PacketReceivedEventHandler SignReceived;
        public static event PacketReceivedEventHandler SetLiquidReceived;
        public static event PacketReceivedEventHandler SpawnSelfReceived;

        public static void LuaInit()
        {
            VersionReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            DisconnectReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ConnectedReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            AvatarReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            InventorySlotReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ReadyReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            WorldParamsReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            RequestChunkReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            StatusReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SendRowReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            CalculateFrameReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SpawnPlayerReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            UpdatePlayerReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            IsPlayerActiveReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            RequestPlayerSyncReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            PlayerLifeReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ChangeTileReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            UpdateTimeReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            DoorChangeReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            UpdateTilesReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            DropItemReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ItemOwnerReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            NpcDataReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            PlayerHitNpcReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ChatMessageReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            PlayerStrikeReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ProjectileReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            StrikeNpcReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            DisposeProjectileReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SetPvpReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            RequestChestReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ChestItemReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            OpenChestReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            KillTileReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            HealEffectReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SetZoneReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            RequestPasswordReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SendPasswordReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            RemoveItemOwnerReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            TalkToNpcReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            PlayerAnimationReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            PlayerManaReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ManaEffectReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            PlayerDeathReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            ChangeTeamsReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            RequestSignReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SignReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SetLiquidReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
            SpawnSelfReceived += new PacketReceivedEventHandler(LuaAddon.CallEvent);
        }

        public static bool GetDataMod(dynamic inst, int start, int length)
        {
            if (NetplayMod.ServerSock[inst.whoAmI].state == 1337)
            {
                // RCON
                // TODO: Code RCON. Lol.
            }
            PacketReceivedEventArgs e = new PacketReceivedEventArgs(inst, start, length);
            if (PacketReceived != null) PacketReceived.Invoke(inst, e);
            if (e.Canceled)
            {
                return false;
            }
            switch (e.PacketType)
            {
                case PacketTypes.Version: if (VersionReceived != null) VersionReceived.Invoke(inst, e); break;
                case PacketTypes.Disconnect: if (DisconnectReceived != null) DisconnectReceived.Invoke(inst, e); break;
                case PacketTypes.Connected: if (ConnectedReceived != null) ConnectedReceived.Invoke(inst, e); break;
                case PacketTypes.Avatar: if (AvatarReceived != null) AvatarReceived.Invoke(inst, e); break;
                case PacketTypes.InventorySlot: if (InventorySlotReceived != null) InventorySlotReceived.Invoke(inst, e); break;
                case PacketTypes.Ready: if (ReadyReceived != null) ReadyReceived.Invoke(inst, e); break;
                case PacketTypes.WorldParams: if (WorldParamsReceived != null) WorldParamsReceived.Invoke(inst, e); break;
                case PacketTypes.RequestChunk: if (RequestChunkReceived != null) RequestChunkReceived.Invoke(inst, e); break;
                case PacketTypes.Status: if (StatusReceived != null) StatusReceived.Invoke(inst, e); break;
                case PacketTypes.SendRow: if (SendRowReceived != null) SendRowReceived.Invoke(inst, e); break;
                case PacketTypes.CalculateFrame: if (CalculateFrameReceived != null) CalculateFrameReceived.Invoke(inst, e); break;
                case PacketTypes.SpawnPlayer: if (SpawnPlayerReceived != null) SpawnPlayerReceived.Invoke(inst, e); break;
                case PacketTypes.UpdatePlayer: if (UpdatePlayerReceived != null) UpdatePlayerReceived.Invoke(inst, e); break;
                case PacketTypes.IsPlayerActive: if (IsPlayerActiveReceived != null) IsPlayerActiveReceived.Invoke(inst, e); break;
                case PacketTypes.RequestPlayerSync: if (RequestPlayerSyncReceived != null) RequestPlayerSyncReceived.Invoke(inst, e); break;
                case PacketTypes.PlayerLife: if (PlayerLifeReceived != null) PlayerLifeReceived.Invoke(inst, e); break;
                case PacketTypes.ChangeTile: if (ChangeTileReceived != null) ChangeTileReceived.Invoke(inst, e); break;
                case PacketTypes.UpdateTime: if (UpdateTimeReceived != null) UpdateTimeReceived.Invoke(inst, e); break;
                case PacketTypes.UpdateDoor: if (DoorChangeReceived != null) DoorChangeReceived.Invoke(inst, e); break;
                case PacketTypes.UpdateTiles: if (UpdateTilesReceived != null) UpdateTilesReceived.Invoke(inst, e); break;
                case PacketTypes.DropItem: if (DropItemReceived != null) DropItemReceived.Invoke(inst, e); break;
                case PacketTypes.ItemOwner: if (ItemOwnerReceived != null) ItemOwnerReceived.Invoke(inst, e); break;
                case PacketTypes.NpcData: if (NpcDataReceived != null) NpcDataReceived.Invoke(inst, e); break;
                case PacketTypes.PlayerHitNpc: if (PlayerHitNpcReceived != null) PlayerHitNpcReceived.Invoke(inst, e); break;
                case PacketTypes.ChatMessage: if (ChatMessageReceived != null) ChatMessageReceived.Invoke(inst, e); break;
                case PacketTypes.PlayerStrike: if (PlayerStrikeReceived != null) PlayerStrikeReceived.Invoke(inst, e); break;
                case PacketTypes.Projectile: if (ProjectileReceived != null) ProjectileReceived.Invoke(inst, e); break;
                case PacketTypes.StrikeNpc: if (StrikeNpcReceived != null) StrikeNpcReceived.Invoke(inst, e); break;
                case PacketTypes.DisposeProjectile: if (DisposeProjectileReceived != null) DisposeProjectileReceived.Invoke(inst, e); break;
                case PacketTypes.SetPvp: if (SetPvpReceived != null) SetPvpReceived.Invoke(inst, e); break;
                case PacketTypes.RequestChest: if (RequestChestReceived != null) RequestChestReceived.Invoke(inst, e); break;
                case PacketTypes.ChestItem: if (ChestItemReceived != null) ChestItemReceived.Invoke(inst, e); break;
                case PacketTypes.OpenChest: if (OpenChestReceived != null) OpenChestReceived.Invoke(inst, e); break;
                case PacketTypes.KillTile: if (KillTileReceived != null) KillTileReceived.Invoke(inst, e); break;
                case PacketTypes.HealEffect: if (HealEffectReceived != null) HealEffectReceived.Invoke(inst, e); break;
                case PacketTypes.SetZone: if (SetZoneReceived != null) SetZoneReceived.Invoke(inst, e); break;
                case PacketTypes.RequestPassword: if (RequestPasswordReceived != null) RequestPasswordReceived.Invoke(inst, e); break;
                case PacketTypes.SendPassword: if (SendPasswordReceived != null) SendPasswordReceived.Invoke(inst, e); break;
                case PacketTypes.RemoveItemOwner: if (RemoveItemOwnerReceived != null) RemoveItemOwnerReceived.Invoke(inst, e); break;
                case PacketTypes.TalkToNpc: if (TalkToNpcReceived != null) TalkToNpcReceived.Invoke(inst, e); break;
                case PacketTypes.PlayerAnimation: if (PlayerAnimationReceived != null) PlayerAnimationReceived.Invoke(inst, e); break;
                case PacketTypes.PlayerMana: if (PlayerManaReceived != null) PlayerManaReceived.Invoke(inst, e); break;
                case PacketTypes.ManaEffect: if (ManaEffectReceived != null) ManaEffectReceived.Invoke(inst, e); break;
                case PacketTypes.PlayerDeath: if (PlayerDeathReceived != null) PlayerDeathReceived.Invoke(inst, e); break;
                case PacketTypes.ChangeTeams: if (ChangeTeamsReceived != null) ChangeTeamsReceived.Invoke(inst, e); break;
                case PacketTypes.RequestSign: if (RequestSignReceived != null) RequestSignReceived.Invoke(inst, e); break;
                case PacketTypes.Sign: if (SignReceived != null) SignReceived.Invoke(inst, e); break;
                case PacketTypes.SetLiquid: if (SetLiquidReceived != null) SetLiquidReceived.Invoke(inst, e); break;
                case PacketTypes.SpawnSelf: if (SpawnSelfReceived != null) SpawnSelfReceived.Invoke(inst, e); break;
            }
            if (e.Canceled)
            {
                return false;
            }

            byte msgType = inst.readBuffer[start];

            try
            {
                if (MainMod.NetMode == 1)
                {
                    switch (msgType)
                    {
                        case 0x3:
                            return OnConnected(inst, start, length);

                        case 0xfe:
                            OnTModTeleport(inst, start, length);
                            return false;

                        default:
                            return true;
                    }
                }
                else if (MainMod.NetMode == 2)
                {
                    if (inst == null)
                    {
                        return false;
                    }
                    byte player = (byte)(int)inst.whoAmI;
                    if (NetplayMod.ServerSock[player] == null)
                    {
                        return false;
                    }
                    if (NetplayMod.ServerSock[player].kill)
                    {
                        return false;
                    }
                    switch (msgType)
                    {
                        case 0x1:
                            return OnVersion(inst, start, length);

                        case 0x4:
                            return OnAvatar(inst, start, length);

                        case 0x5:
                            return OnPlayerSlot(inst, start, length);

                        case 0x6:
                            return OnPlayerReady(inst, start, length);

                        case 0xa:
                            return OnSendSection(inst, start, length);

                        case 0xc:
                            return OnPlayerSpawn(inst, start, length);

                        case 0xd:
                            return OnPlayerUpdate(inst, start, length);

                        case 0xf:
                            return OnRequestSync(inst, start, length);

                        case 0x10:
                            return OnPlayerHealth(inst, start, length);

                        case 0x11:
                            return OnTileChange(inst, start, length);

                        case 0x14:
                            return OnSendTileSquare(inst, start, length);

                        case 0x17:
                            return OnSpawnNpc(inst, start, length);

                        case 0x19:
                            return OnChat(inst, start, length);

                        case 0x1a:
                            return OnPlayerDamage(inst, start, length);

                        case 0x1b:
                            return OnNewProjectile(inst, start, length);

                        case 0x1e:
                            return OnTogglePvp(inst, start, length);

                        case 0x1f:
                            return OnChestGetContents(inst, start, length);

                        case 0x20:
                            return OnChestItem(inst, start, length);

                        case 0x21:
                            return OnChestOpen(inst, start, length);

                        case 0x22:
                            return OnKillTile(inst, start, length);

                        case 0x26:
                            return OnServerPassword(inst, start, length);

                        case 0x2a:
                            return OnPlayerMana(inst, start, length);

                        case 0x2c:
                            return OnKillMe(inst, start, length);

                        case 0x2d:
                            return OnChangeTeam(inst, start, length);

                        case 0x2e:
                            return OnReadSign(inst, start, length);

                        case 0x2f:
                            return OnNewSign(inst, start, length);

                        case 0x30:
                            return OnLiquid(inst, start, length);

                        case 0xff:
                            OnTModIdentify(inst, start, length);
                            return false;

                        default:
                            return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                try
                {
                    MainMod.Log("A message buffer exception occured. Please report this at http://tmod.biz/forums/");
                    using (StreamWriter writer = new StreamWriter("messageBufferException.txt", true))
                    {
                        writer.WriteLine(DateTime.Now);
                        writer.WriteLine(exception);
                        writer.WriteLine("");
                    }
                    if (e.PacketType == PacketTypes.ChatMessage)
                        return false; // workaround for annoying command error spam.
                }
                catch
                {
                }
                return true;
            }
        }

        private static void OnTModTeleport(dynamic inst, int start, int length)
        {
            int index = start + 1;
            int x = BitConverter.ToInt32(inst.readBuffer, index);
            index += sizeof(int);
            int y = BitConverter.ToInt32(inst.readBuffer, index);
            MainMod.NewText("Teleporting to (" + x + ", " + y + ")", 175, 75, 255);
            MainMod.Player[MainMod.MyPlayer].position = new Vector2((float)x * 16f, (float)y * 16f);
        }

        private static void OnTModIdentify(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            Session.Sessions[player].IsUsingTMod = true;
            MainMod.Notice("IP " + Session.Sessions[player].IpAddress + " is using tMod");
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return;
            }
        }

        private static bool OnConnected(dynamic inst, int start, int length)
        {
#if !ZIDONUKE
            byte[] writeBuffer = new byte[5];
            Buffer.BlockCopy(BitConverter.GetBytes(1), 0, writeBuffer, 0, 4);
            writeBuffer[4] = 0xff;
            NetMessageMod.FlushBuffer(writeBuffer);
#endif
            return true;
        }

        private static bool OnPlayerReady(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                MainMod.Notice("Op/Mod " + MainMod.Player[player].name + " has joined from " + Session.Sessions[player].IpAddress);
                return true;
            }
            if (MainMod.Config.AllowRegistrationPasswordForServerPassword && !string.IsNullOrWhiteSpace(NetplayMod.password))
            {
                NetplayMod.ServerSock[player].state = -1;
                NetMessageMod.SendData(0x25, player, -1, "", 0, 0f, 0f, 0f);
                return false;
            }
            if ((MainMod.Config.RequireNewCharacterIfNotRegistered && !Database.IsUserRegistered(MainMod.Player[player].name)) || MainMod.Config.RequireNewCharacterAlways)
            {
                int health = MainMod.Player[player].statLife;
                int maxhealth = MainMod.Player[player].statLifeMax;
                int mana = MainMod.Player[player].statMana;
                int maxmana = MainMod.Player[player].statManaMax;
                if (health != 100 || maxhealth != 100 || mana != 0 || maxmana != 0)
                {
                    NetplayMod.KickPlayer(player, "You need to create a new character to join this server. Error (0x01)");
                    return false;
                }
                for (int num = 2; num < 0x2c; num++)
                {
                    if (num < 11)
                    {
                        if (MainMod.Player[player].armor[num].name != "")
                        {
                            NetplayMod.KickPlayer(player, "You need to create a new character to join this server. Error (0x02)");
                            return false;
                        }
                    }
                    if (MainMod.Player[player].inventory[num].name != "")
                    {
                        NetplayMod.KickPlayer(player, "You need to create a new character to join this server. Error (0x03)");
                        return false;
                    }
                }
                if (MainMod.Player[player].inventory[0].name != "Copper Pickaxe" || MainMod.Player[player].inventory[1].name != "Copper Axe")
                {
                    NetplayMod.KickPlayer(player, "You need to create a new character to join this server. Error (0x04)");
                    return false;
                }
            }
            MainMod.Notice(MainMod.Player[player].name + " has joined from " + Session.Sessions[player].IpAddress);
            return true;
        }

        private static bool OnKillMe(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            MainMod.Notice(MainMod.Player[player].name + " has died.");
            return true;
        }

        private static bool OnPlayerSpawn(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            MainMod.Notice(MainMod.Player[player].name + " has respawned.");
            return true;
        }

        private static bool OnVersion(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            string ipaddr = ((IPEndPoint)NetplayMod.ServerSock[player].tcpClient.Client.RemoteEndPoint).Address.ToString();
            if (NetplayMod.CheckBan(ipaddr))
            {
                MainMod.Log("Banned IP " + Session.Sessions[player].IpAddress + " attempted to connect");
                NetMessageMod.SendData(0x2, player, -1, "You are banned from this server.", 0, 0f, 0f, 0f);
                return false;
            }
            Session.Sessions[player] = new Session(player, ipaddr);
            int count = 0;
            for (int i = 0; i < MainMod.Player.Length; i++)
            {
                if (MainMod.Player[i] != null && MainMod.Player[i].active)
                {
                    count++;
                }
            }
            if (count >= MainMod.Config.PlayerSlots && (!MainMod.IsOp(player) || !MainMod.IsMod(player)))
            {
                NetMessageMod.SendData(0x2, player, -1, "The server is full. (" + count + "/" + MainMod.Config.PlayerSlots + ") Online", 0, 0f, 0f, 0f);
                return false;
            }
            if (NetplayMod.ServerSock[player].state == 0)
            {
                if (Encoding.ASCII.GetString(inst.readBuffer, start + 1, length - 1) == "tModRCON001")
                {
                    // This tells us that the user is using the tMod RCON.
                    NetplayMod.ServerSock[player].state = 1337; // you think of a better number that isn't 42.
                    return false; // stop TerrariaServer from moaning
                }
                else if (Encoding.ASCII.GetString(inst.readBuffer, start + 1, length - 1) == ("Terraria" + MainMod.curRelease))
                {
                    if (!MainMod.Config.AllowRegistrationPasswordForServerPassword)
                    {
                        if (string.IsNullOrWhiteSpace(NetplayMod.password) || MainMod.IsOp(player) || MainMod.IsMod(player))
                        {
                            NetplayMod.ServerSock[player].state = 1;
                            NetMessageMod.SendData(3, player, -1, "", 0, 0f, 0f, 0f);
                        }
                        else
                        {
                            NetplayMod.ServerSock[player].state = -1;
                            NetMessageMod.SendData(0x25, player, -1, "", 0, 0f, 0f, 0f);
                        }
                    }
                    else
                    {
                        NetplayMod.ServerSock[player].state = 1;
                        NetMessageMod.SendData(3, player, -1, "", 0, 0f, 0f, 0f);
                    }
                    MainMod.Log("IP " + Session.Sessions[player].IpAddress + " is connecting");
                }
                else
                {
                    NetplayMod.KickPlayer(player, "Invalid Client Version");
                    return false;
                }
            }
            return false;
        }

        private static bool OnSpawnNpc(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            MainMod.Notice(MainMod.Player[player].name + " has spawned a NPC", MainMod.Config.ShowNPCNotifications);
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (MainMod.Config.KickSpawnNpc)
            {
                NetplayMod.KickPlayer(player, "You cannot spawn NPCs on this server");
                return false;
            }

            return MainMod.Config.AllowSpawnNpcs;
        }

        private static bool OnSendSection(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            NetplayMod.KickPlayer(player, "Hacking (0x01)");
            return false;
        }

        private static bool OnSendTileSquare(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            short size = BitConverter.ToInt16(inst.readBuffer, start + 1);
            int x = BitConverter.ToInt32(inst.readBuffer, start + 3);
            int y = BitConverter.ToInt32(inst.readBuffer, start + 7);
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.BlockSendTileSquare) return false;
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                NetMessageMod.SendTileSquare(player, x, y, size);
                return false;
            }
            if (size > 5)
            {
                NetplayMod.KickPlayer(player, "Hacking (0x02)");
                return false;
            }
            int index = start + 11;
            for (int lx = x; lx < (x + size); lx++)
            {
                for (int ly = y; ly < (y + size); ly++)
                {
                    //Read tile info
                    byte flags = inst.readBuffer[index];
                    index++;
                    byte newtype = inst.readBuffer[index];
                    index++;
                    byte newwall = inst.readBuffer[index];
                    index++;
                    byte newliquid = inst.readBuffer[index];
                    index++;
                    bool newlava = BitConverter.ToBoolean(inst.readBuffer, index);
                    index++;

                    if (!CanChangeTile(player, lx, ly, flags, newtype, newwall, newliquid, newlava))
                    {
                        NetMessageMod.SendTileSquare(player, x, y, size);
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool OnLiquid(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            int index = start + 1;
            int x = BitConverter.ToInt32(inst.readBuffer, index);
            index += 4;
            int y = BitConverter.ToInt32(inst.readBuffer, index);
            index += 4;
            byte liquid = inst.readBuffer[index];
            index++;
            byte l = inst.readBuffer[index];
            index++;
            bool lava = false;
            if (l == 1)
            {
                lava = true;
            }
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                NetMessageMod.SendTileSquare(player, x, y, 10);
                return false;
            }
            if (!MainMod.Config.AllowLava && lava)
            {
                NetMessageMod.SendTileSquare(player, x, y, 10);
                return false;
            }
            if (!MainMod.Config.AllowWater && !lava)
            {
                NetMessageMod.SendTileSquare(player, x, y, 10);
                return false;
            }
            if (MainMod.Config.SpawnProtectionRadius > 0)
            {
                Vector2 tile = new Vector2(x, y);
                Vector2 spawn = new Vector2(MainMod.SpawnTileX, MainMod.SpawnTileY);
                var distance = Vector2.Distance(spawn, tile);
                if (distance < MainMod.Config.SpawnProtectionRadius)
                {
                    NetMessageMod.SendTileSquare(player, x, y, 10);
                    return false;
                }
            }
            bool bucket = false;
            for (int i = 0; i < 44; i++)
            {
                if (MainMod.Player[player].inventory[i].type >= 205 && MainMod.Player[player].inventory[i].type <= 207)
                {
                    bucket = true;
                }
            }
            if (MainMod.Config.KickLiquidHack && !bucket)
            {
                NetplayMod.KickPlayer(player, "Hacking (0x03)");
                return false;
            }
            if (SetLiquid != null)
            {
                SetLiquidEventArgs e = new SetLiquidEventArgs(player, x, y, liquid, lava);
                SetLiquid.Invoke(null, e);
                if (e.Canceled)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool OnAvatar(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            int hair = inst.readBuffer[start + 2];
            if (hair >= MainMod.MaxHair)
            {
                NetplayMod.KickPlayer(player, "Hacking (0x04)");
                return false;
            }
            byte hardcore = inst.readBuffer[start + 25];
            int index = start + 26;
            string name = Encoding.ASCII.GetString(inst.readBuffer, index, (length - index) + start);
            if (MainMod.Config.KickInvalidName && (name.Length > 24 || name.Length < 1 || name.Trim().Length == 0 || string.IsNullOrWhiteSpace(name)))
            {
                MainMod.Log("Invalid username from " + Session.Sessions[player].IpAddress + " using (" + name + ")");
                NetplayMod.KickPlayer(player, "Please use a vaild name");
                return false;
            }
            if (MainMod.Config.KickImpersonatingName && name.Trim() != name)
            {
                MainMod.Log("Impersonating username from " + Session.Sessions[player].IpAddress + " using (" + name + ")");
                NetplayMod.KickPlayer(player, "Please use a name without leading/trailing whitespace");
                return false;
            }
            if (MainMod.Config.RequireAlphaNumericName && name.Trim("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_ ".ToCharArray()) != "")
            {
                MainMod.Log("Invalid username from " + Session.Sessions[player].IpAddress + " using (" + name + ")");
                NetplayMod.KickPlayer(player, "Please use an alphanumeric name");
                return false;
            }
            Session.Sessions[player].Initialize(name);
            /*if (MainMod.Config.RequireHardcoreCharacter && !hardcore && !(MainMod.IsOp(player) || MainMod.IsMod(player)))
            {
                NetplayMod.KickPlayer(player, "Please use a hardcore character to play on this server");
                return false;
            }*/
            // Require hardcore temporarily broken.
            return true;
        }

        private static bool OnPlayerUpdate(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            return true;
        }

        private static bool OnPlayerHealth(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.KickHealthHack)
            {
                int index = start + 1;
                byte target = inst.readBuffer[index];
                if (target == player)
                {
                    index++;
                    int health = BitConverter.ToInt16(inst.readBuffer, index);
                    index += 2;
                    int healthMax = BitConverter.ToInt16(inst.readBuffer, index);
                    if (health > 1000 || healthMax > 1000)
                    {
                        NetplayMod.KickPlayer(player, "Health hack detected.");
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool OnDoorChange(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.RequireRegistrationForDoors && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            int index = start + 1;
            bool action = BitConverter.ToBoolean(inst.readBuffer, index);
            index++;
            int x = BitConverter.ToInt32(inst.readBuffer, index);
            index += 4;
            int y = BitConverter.ToInt32(inst.readBuffer, index);
            if (DoorChanged != null)
            {
                DoorChangedEventArgs e = new DoorChangedEventArgs(player, action, x, y);
                DoorChanged.Invoke(null, e);
                if (e.Canceled)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool OnPlayerSlot(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.KickItemStackHack)
            {
                byte target = inst.readBuffer[start + 1];
                if (target == player)
                {
                    int slot = inst.readBuffer[start + 2];
                    int stack = inst.readBuffer[start + 3];
                    string name = Encoding.ASCII.GetString(inst.readBuffer, start + 4, length - 4);
                    dynamic item = ItemMod.Item.GetConstructor(new Type[0]).Invoke(new object[0]);
                    item.SetDefaults(name.ToProper());
                    if (item.type > 0)
                    {
                        if (stack > item.maxStack)
                        {
                            NetplayMod.KickPlayer(player, "Item stack hack detected. Create a new character.");
                            return false;
                        }
                    }
                    if (stack > 250)
                    {
                        NetplayMod.KickPlayer(player, "Item stack hack detected. Create a new character.");
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool OnDropItem(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowPlayerItemDropping)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            return true;
        }

        private static bool OnPlayerDamage(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }

            int index = start + 1;
            byte target = inst.readBuffer[index];
            if (target != player)
            {
                if (!MainMod.Player[target].hostile)
                {
                    return false;
                }
                index += 2;
                int damage = BitConverter.ToInt16(inst.readBuffer, index);
                if (damage > 250 && MainMod.Config.KickDamageHack)
                {
                    NetplayMod.KickPlayer(player, "Hacking (0x06)");
                    return false;
                }
                if (damage > 120 && (!MainMod.Config.AllowExplosives || !MainMod.Config.AllowAbove150Damage))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool OnPlayerMana(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.KickManaHack)
            {
                int index = start + 1;
                byte target = inst.readBuffer[index];
                if (target == player)
                {
                    index++;
                    int mana = BitConverter.ToInt16(inst.readBuffer, index);
                    index += 2;
                    int manaMax = BitConverter.ToInt16(inst.readBuffer, index);
                    if (mana > 300 || manaMax > 300)
                    {
                        NetplayMod.KickPlayer(player, "Mana hack - Create a new character.");
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool OnChestGetContents(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowChest)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            return true;
        }

        private static bool OnChestItem(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowChest)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            return true;
        }

        private static bool OnChestOpen(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowChest)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            return true;
        }

        private static bool OnReadSign(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            return true;
        }

        private static bool OnNewSign(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowSignEditing)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (MainMod.Config.SpawnProtectionRadius > 0)
            {
                int index = start + 1;
                int sign = BitConverter.ToInt16(inst.readBuffer, index);
                index += 2;
                int x = BitConverter.ToInt32(inst.readBuffer, index);
                index += 4;
                int y = BitConverter.ToInt32(inst.readBuffer, index);
                index += 4;
                Vector2 tile = new Vector2(x, y);
                Vector2 spawn = new Vector2(MainMod.SpawnTileX, MainMod.SpawnTileY);
                var distance = Vector2.Distance(spawn, tile);
                if (distance < MainMod.Config.SpawnProtectionRadius)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool OnTogglePvp(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (MainMod.Config.Peaceful)
            {
                return false;
            }
            return true;
        }

        private static bool OnChangeTeam(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            return true;
        }

        private static bool OnNewProjectile(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }

            int index = start + 1;
            short id = BitConverter.ToInt16(inst.readBuffer, index);
            index += 2;
            float x = BitConverter.ToSingle(inst.readBuffer, index);
            index += 4;
            float y = BitConverter.ToSingle(inst.readBuffer, index);
            index += 4;
            float vx = BitConverter.ToSingle(inst.readBuffer, index);
            index += 4;
            float vy = BitConverter.ToSingle(inst.readBuffer, index);
            index += 4;
            float knockback = BitConverter.ToSingle(inst.readBuffer, index);
            index += 4;
            short damage = BitConverter.ToInt16(inst.readBuffer, index);
            index += 2;
            byte owner = inst.readBuffer[index];
            index++;
            byte type = inst.readBuffer[index];
            index++;



            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                NetMessageMod.SendData(0x1d, player, -1, "", id, (float)owner, 0f, 0f);
                return false;
            }


            if (!MainMod.Config.AllowChest)//.AllowSandGuns)
            {

            }

            if (!MainMod.Config.AllowExplosives && (type == 28 || type == 29 || type == 30 || type == 37))
            {
                if (MainMod.Config.KickExplosives)
                {
                    NetplayMod.KickPlayer(player, "Explosives are disallowed on this server");
                }
                return false;
            }
            return true;
        }

        private static bool OnTileChange(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            int index = start + 1;
            byte action = inst.readBuffer[index];
            index++;
            int x = BitConverter.ToInt32(inst.readBuffer, index);
            index += 4;
            int y = BitConverter.ToInt32(inst.readBuffer, index);
            index += 4;
            byte type = inst.readBuffer[index];

            bool retval;
            if (Session.Sessions[player].IsCheckingBlock)
            {
                Session.Sessions[player].IsCheckingBlock = false;
                Database.Check(x, y, Session.Sessions[player]);
                retval = false;
            }
            else
            {
                retval = CanPerformAction(player, x, y, action, action == 1 || action == 3 ? type : (action == 2 ? MainMod.Tile[x, y].wall : MainMod.Tile[x, y].type));
            }

            if (!retval)
            {
                NetMessageMod.SendTileSquare(player, x, y, 3);
                return false;
            }

            if (TileChanged != null)
            {
                TileChangedEventArgs e = new TileChangedEventArgs(player, action, x, y, type);
                TileChanged.Invoke(null, e);
                if (e.Canceled)
                {
                    return false;
                }
            }

            Session.Sessions[player].MakeEdit(new Edit
            {
                X = x,
                Y = y,
                Type = action == 1 || action == 3 ? type : (MainMod.Tile[x, y] == null ? 0 : (action == 2 ? MainMod.Tile[x, y].wall : MainMod.Tile[x, y].type)),
                Action = action,
            });
            return true;
        }

        private static void ReverseEdit(int player, int x, int y, byte action, byte type)
        {
            NetMessageMod.SendData(0x11, player, -1, "", Edit.ReverseAction(action), x, y, type);
        }

        private static bool CanPerformAction(byte player, int x, int y, byte action, byte type)
        {
            if (!Session.Sessions[player].Group.CanBuild)
            {
                Session.Sessions[player].SendText("You do not have building permissions!");
                return false;
            }
            switch (action)
            {
                case 0:
                case 4:
                    return CanDestroy(player, x, y, type, false);

                case 2:
                    return CanDestroy(player, x, y, type, true);

                case 1:
                    return CanCreate(player, x, y, type, false);

                case 3:
                    return CanCreate(player, x, y, type, true);

                default:
                    return true;
            }
        }

        private static bool CanChangeTile(byte player, int x, int y, byte flags, byte type, byte wall, byte liquid, bool lava)
        {
            if (!Session.Sessions[player].Group.CanBuild)
            {
                Session.Sessions[player].SendText("You do not have building permissions!");
                return false;
            }
            if (MainMod.Tile[x, y] == null)
            {
                return CanCreate(player, x, y, type, false);
            }
            //If Active
            if ((flags & 1) == 1)
            {
                //If Tile Changed
                if (MainMod.Tile[x, y].type != type)
                {
                    if (type == 48 && MainMod.Config.KickSpikeHack)
                    {
                        NetplayMod.KickPlayer(player, "Hacking (0x07)");
                        return false;
                    }
                    if (!CanCreate(player, x, y, type, false))
                    {
                        return false;
                    }
                }
            }
            else
            {
                //If Tile Changed
                if (MainMod.Tile[x, y].type != type)
                {
                    if (!CanDestroy(player, x, y, MainMod.Tile[x, y].type, false))
                    {
                        return false;
                    }
                }
            }
            //If Wall
            if ((flags & 4) == 4)
            {
                //If Wall Changed
                if (MainMod.Tile[x, y].wall != wall)
                {
                    if (!CanCreate(player, x, y, wall, true))
                    {
                        return false;
                    }
                }
            }
            else
            {
                //If Wall Changed
                if (MainMod.Tile[x, y].wall != wall)
                {
                    if (!CanDestroy(player, x, y, MainMod.Tile[x, y].wall, true))
                    {
                        return false;
                    }
                }
            }
            //If Liquid
            if ((flags & 8) == 8)
            {
                //If Liquid Changed
                if (MainMod.Tile[x, y].liquid != liquid)
                {
                    if (!CanChangeLiquid(player, x, y, liquid))
                    {
                        return false;
                    }
                }
                if (MainMod.Tile[x, y].lava != lava)
                {
                    if (!CanChangeLava(player, x, y, liquid, lava))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (MainMod.Tile[x, y].liquid != liquid)
                {
                    if (!CanChangeLiquid(player, x, y, 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool OnKillTile(dynamic inst, int start, int length)
        {
            int index = start + 1;
            byte player = (byte)(int)inst.whoAmI;
            int x = BitConverter.ToInt32(inst.readBuffer, index);
            index += 4;
            int y = BitConverter.ToInt32(inst.readBuffer, index);
            if (CanDestroy(player, x, y, MainMod.Tile[x, y].type, false))
            {
                if (MainMod.Tile[x, y].type == 0x15)
                {
                    NetMessageMod.SendData(0x11, -1, -1, "", 0, (float)x, (float)y, 0f);
                    Session.Sessions[player].MakeEdit(new Edit
                    {
                        X = x,
                        Y = y,
                        Type = MainMod.Tile[x, y] == null ? 0 : MainMod.Tile[x, y].type,
                        Action = 0,
                    });
                    WorldGenMod.KillTile(x, y, false, false, false);
                }
                else
                {
                    if (MainMod.Config.KickInvalidTileEdits && !MainMod.IsOp(player) && !MainMod.IsMod(player))
                        NetplayMod.KickPlayer(player, "Hacking (0x08)");
                    return false;
                }
            }
            return false;
        }

        private static bool CanCreate(byte player, int x, int y, byte type, bool wall)
        {
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowConstruction)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (!MainMod.Config.AllowSpikes && type == 48)
            {
                return false;
            }
            if (MainMod.Config.SpawnProtectionRadius > 0)
            {
                Vector2 tile = new Vector2(x, y);
                Vector2 spawn = new Vector2(MainMod.SpawnTileX, MainMod.SpawnTileY);
                var distance = Vector2.Distance(spawn, tile);
                if (distance < MainMod.Config.SpawnProtectionRadius)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CanDestroy(byte player, int x, int y, byte type, bool wall)
        {
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowDestruction)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (MainMod.Config.SpawnProtectionRadius > 0)
            {
                Vector2 tile = new Vector2(x, y);
                Vector2 spawn = new Vector2(MainMod.SpawnTileX, MainMod.SpawnTileY);
                var distance = Vector2.Distance(spawn, tile);
                if (distance < MainMod.Config.SpawnProtectionRadius)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CanChangeLiquid(byte player, int x, int y, byte liquid)
        {
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if (!MainMod.Config.AllowLava || !MainMod.Config.AllowWater)
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (MainMod.Config.SpawnProtectionRadius > 0)
            {
                Vector2 tile = new Vector2(x, y);
                Vector2 spawn = new Vector2(MainMod.SpawnTileX, MainMod.SpawnTileY);
                var distance = Vector2.Distance(spawn, tile);
                if (distance < MainMod.Config.SpawnProtectionRadius)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CanChangeLava(byte player, int x, int y, byte liquid, bool lava)
        {
            if (MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                return true;
            }
            if ((lava && !MainMod.Config.AllowLava) || (!lava && !MainMod.Config.AllowWater))
            {
                return false;
            }
            if (MainMod.Config.RequireRegistration && !Session.Sessions[player].IsLoggedIn)
            {
                return false;
            }
            if (MainMod.Config.SpawnProtectionRadius > 0)
            {
                Vector2 tile = new Vector2(x, y);
                Vector2 spawn = new Vector2(MainMod.SpawnTileX, MainMod.SpawnTileY);
                var distance = Vector2.Distance(spawn, tile);
                if (distance < MainMod.Config.SpawnProtectionRadius)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool OnRequestSync(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            return true;
        }

        private static bool OnServerPassword(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            int index = start + 1;
            string password = Encoding.ASCII.GetString(inst.readBuffer, index, (length - index) + start);
            if (string.IsNullOrWhiteSpace(NetplayMod.password))
            {
                MainMod.Notice("IP " + Session.Sessions[player].IpAddress + " attempted to use a server password when not needed.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MainMod.Notice("Invalid Password from " + Session.Sessions[player].IpAddress);
                NetMessageMod.SendData(2, player, -1, "Invalid password.", 0, 0f, 0f, 0f);
                return false;
            }
            else if (password == MainMod.Config.OpMePassword && password != "Disabled")
            {
                MainMod.Notice("OpMe Password accepted from " + Session.Sessions[player].IpAddress + " on password, giving op access.", MainMod.Config.ShowOpMeNotifications);
                MainMod.Config.Ops.Add(Session.Sessions[player].IpAddress);
                MainMod.Config.Mods.Remove(Session.Sessions[player].IpAddress);
                MainMod.SaveConfig();
                if (MainMod.Config.AllowRegistrationPasswordForServerPassword)
                {
                    NetplayMod.ServerSock[player].state = 2;
                    NetMessageMod.SendData(7, player, -1, "", 0, 0f, 0f, 0f);
                    return false;
                }
                else
                {
                    NetplayMod.ServerSock[player].state = 1;
                    NetMessageMod.SendData(3, player, -1, "", 0, 0f, 0f, 0f);
                    return false;
                }
            }
            else if (password == NetplayMod.password || MainMod.Config.AlternativePasswords.Contains(password) || MainMod.IsOp(player) || MainMod.IsMod(player))
            {
                if (MainMod.Config.AllowRegistrationPasswordForServerPassword)
                {
                    NetplayMod.ServerSock[player].state = 2;
                    NetMessageMod.SendData(7, player, -1, "", 0, 0f, 0f, 0f);
                    return false;
                }
                else
                {
                    NetplayMod.ServerSock[player].state = 1;
                    NetMessageMod.SendData(3, player, -1, "", 0, 0f, 0f, 0f);
                    return false;
                }
            }
            else if (MainMod.Config.AllowRegistrationPasswordForServerPassword)
            {
                Session.Sessions[player].Login(Database.Password(password));
                if (Session.Sessions[player].IsLoggedIn)
                {
                    NetplayMod.ServerSock[player].state = 2;
                    NetMessageMod.SendData(7, player, -1, "", 0, 0f, 0f, 0f);
                    return false;
                }
            }
            MainMod.Notice("Incorrect Password from " + Session.Sessions[player].IpAddress + " using " + password);
            NetMessageMod.SendData(2, player, -1, "Incorrect password.", 0, 0f, 0f, 0f);
            return false;
        }

        private static bool OnChat(dynamic inst, int start, int length)
        {
            byte player = (byte)(int)inst.whoAmI;
            byte r = inst.readBuffer[start + 2];
            byte g = inst.readBuffer[start + 3];
            byte b = inst.readBuffer[start + 4];
            string msg = Encoding.ASCII.GetString(inst.readBuffer, start + 5, length - 5);
            if (string.IsNullOrWhiteSpace(msg))
            {
                return false;
            }
            if (!msg.IsClean())
            {
                MainMod.Notice("Potential crash exploit detected. User: " + Session.Sessions[player].Username);
                Session.Sessions[player].SendError("Warning: invalid characters detected.");
                return false;
            }
            if (!MainMod.ServerModCommand(player, msg))
            {
                if (Session.Sessions[player].Butterfingers)
                {
                    char[] text = msg.ToCharArray();
                    for (int i = 0; i < text.Count(); i++)
                    {
                        try
                        {
                            if (WorldGenMod.GenRand.Next(10) == 3) text[i] = (char)(((int)text[i]) + 1);
                        }
                        catch { } // meh, lazy mood
                    }
                    msg = new string(text);
                }
                if (Session.Sessions[player].Stupidized)
                {
                    string[] words = msg.ToLower().Replace(",", "").Replace(".", "").Split(' ');
                    for (int i = 0; i < words.Count(); i++)
                    {
                        switch (words[i])
                        {
                            case "hi":
                            case "hello":
                            case "greetings":
                            case "hai":
                                words[i] = randomString("hallo", "ello", "hullo");
                            break;

                            case "you":
                            if (words.Length-1 > i && words[i + 1] == "are")
                            {
                                words[i + 1] = "";
                                words[i] = randomString("u", "ur", "yur", "yer", "er", "you'r");
                            }
                            else
                            {
                                words[i] = randomString("u", "yu", "uu", "uze");
                            }
                            break;
                                
                            case "your":
                            case "you're":
                            case "youre":
                                words[i] = randomString("u", "ur", "yur", "yer", "er", "you'r");
                            break;

                            case "is":
                                words[i] = "am";
                            break;
                                
                            case "am":
                                words[i] = "em";
                            break;

                            case "why":
                                words[i] = randomString("y", "wy", "wai");
                            break;

                            case "computer":
                                words[i] = randomString("puter", "pooter", "comp", "comptooter", "macintosh");
                            break;

                            case "how":
                            words[i] = "hw";
                            break;

                            case "are":
                                words[i] = randomString("r", "be");
                            break;

                            case "code":
			                case "codes":
			                case "word":
			                case "words":
			                case "sentance":
			                case "message":
			                case "messages":
			                case "sentances":
				                words[i] = randomString("squiggles", "squigglies", "squiggly lines");
				            break;

                            case "the":
                                words[i] = randomString("teh", "tuh", "duh", "d", "de");
                            break;

                            case "hate":
			                case "evil":
			                case "dislike":
			                case "bad":
				                words[i] = "no like";
				            break;

                            case "don't":
                            case "donot":
                            case "dont":
                                words[i] = "no";
                            break;

                            case "do":
                            if (words.Length-1 > i && words[i + 1] == "not")
                            {
                                words[i + 1] = "";
                                words[i] = "no";
                            }
                            else
                            {
                                words[i] = "ye";
                            }
                            break;

                            case "lol":
                            case "rofl":
                            case "haha":
                            case "lmfao":
                            case "lmao":
                            case "roflmao":
                                words[i] = randomString("hahahahaha", "ha ha ha", "haaaaaaaaaaaa", "hahahahahahahaaaaa", "iggdigaiohgagdi");
                            break;

                            case "yes":
                            case "yeah":
                            case "yep":
                            case "yeh":
                            case "yuh":
                                words[i] = randomString("yah", "yeh", "noo");
                            break;

                            case "off":
                                words[i] = "of";
                            break;

                            case "this":
                                words[i] = "dis";
                            break;

                            case "my":
                                words[i] = "me";
                            break;

                            default:
                                char[] text = words[i].ToCharArray();
                                for (int i1 = 0; i1 < text.Count(); i1++)
                                {
                                    try
                                    {
                                        if (WorldGenMod.GenRand.Next(20) == 3) text[i1] = (char)(((int)text[i1]) + 1);
                                    }
                                    catch { } // meh, lazy mood
                                }
                                words[i] = new string(text);
                            break;
                        }
                        msg = string.Join(" ", words);
                    }
                }
                if (Session.Sessions[player].Group.ChatPrefix != "")
                {
                    msg = Session.Sessions[player].Group.ChatPrefix + " <" + Session.Sessions[player].Username + "> " + msg;
                    NetMessageMod.BroadcastMessage(Session.Sessions[player].Group.ChatColor.R, Session.Sessions[player].Group.ChatColor.G, Session.Sessions[player].Group.ChatColor.B, msg);
                    return false;
                }
                if (Session.Sessions[player].IsMuted)
                {
                    Session.Sessions[player].SendError("You cannot speak while muted.");
                    return false;
                }
                if (!MainMod.Config.RequireRegistrationForChat || Session.Sessions[player].IsLoggedIn || MainMod.IsOp(player) || MainMod.IsMod(player))
                {
                    MainMod.Notice(MainMod.Player[player].name + ": " + msg);
                    msg = MainMod.Player[player].name + ": " + msg;
                    if (MainMod.Config.ForceChatColors)
                    {
                        if (MainMod.IsOp(player))
                        {
                            NetMessageMod.SendData(0x19, -1, -1, msg, 0xff, MainMod.Config.OpColor.R, MainMod.Config.OpColor.G, MainMod.Config.OpColor.B);
                        }
                        else if (MainMod.IsMod(player))
                        {
                            NetMessageMod.SendData(0x19, -1, -1, msg, 0xff, MainMod.Config.ModColor.R, MainMod.Config.ModColor.G, MainMod.Config.ModColor.B);
                        }
                        else if (Session.Sessions[player].IsLoggedIn)
                        {
                            NetMessageMod.SendData(0x19, -1, -1, msg, 0xff, Session.Sessions[player].Group.ChatColor.R, Session.Sessions[player].Group.ChatColor.G, Session.Sessions[player].Group.ChatColor.B);
                        }
                        else
                        {
                            NetMessageMod.SendData(0x19, -1, -1, msg, 0xff, MainMod.Config.GuestColor.R, MainMod.Config.GuestColor.G, MainMod.Config.GuestColor.B);
                        }
                    }
                    else
                    {
                        NetMessageMod.SendData(0x19, -1, -1, msg, 0xff, (float)r, (float)g, (float)b);
                    }
                }
                else
                {
                    Session.Sessions[player].SendError("You cannot speak while not logged in.");
                }
            }
            return false;
        }

        public static string randomString(params string[] args)
        {
            return args[WorldGenMod.GenRand.Next(args.Length)];
        }
    }
}
