using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Microsoft.Xna.Framework;

namespace tMod_v3
{
    public class Session
    {
        public static Session[] Sessions = new Session[0xff];

        public int Id { get; private set; }
        public int Index { get; private set; }
        public int DbId { get; private set; }
        public string Username { get; private set; }
        public string IpAddress { get; private set; }
        public bool IsLoggedIn { get; private set; }
        public bool IsCheckingBlock { get; set; }
        public bool IsTeleporting { get; set; }
        public bool IsUsingTMod { get; set; }
        public bool IsAllowedToRegister { get; set; }
        public bool IsMuted { get; set; }
        public bool Stupidized { get; set; }
        public bool Butterfingers { get; set; }
        public Group Group { get; set; }
        public int OldSpawnX { get; set; }
        public int OldSpawnY { get; set; }
        public int TeleSpawnX { get; set; }
        public int TeleSpawnY { get; set; }
        public int SpawnX { get { return MainMod.Player[Index].SpawnX; } }
        public int SpawnY { get { return MainMod.Player[Index].SpawnY; } }

        public Session(int index, string ip)
        {
            Index = index;
            IpAddress = ip;
            Group = MainMod.Groups.Member;
        }

        public Session(int index, string username, string ip, int id)
        {
            Index = index;
            Username = username;
            IpAddress = ip;
            Id = id;
        }

        internal void Initialize(string username)
        {
            Id = Database.InsertSession(Username = username, IpAddress);
        }

        public void SendText(string text)
        {
            SendText(175, 75, 255, text);
        }

        public void SendError(string text)
        {
            SendText(255, 0, 0, text);
        }

        public void SendHelp(string text)
        {
            SendText(255, 240, 20, text);
        }

        public void SendText(byte r, byte g, byte b, string text, int from = 0xff)
        {
            NetMessageMod.SendData(0x19, Index, -1, text, from, r, g, b);
        }

        public void MakeEdit(Edit edit)
        {
            Database.InsertEdit(Id, edit.Action, edit.Type, edit.X, edit.Y);
        }

        public void Rollback()
        {
            Database.Rollback(Id);
        }

        public void Register(string hash)
        {
            if (MainMod.Config.BlockRegistration && !IsAllowedToRegister)
            {
                SendError("You are not allowed to register at this time.");
                return;
            }
            if (!Database.IsUserRegistered(Username))
            {
                if (hash == "")
                {
                    SendError("Registration failed. Blank password?");
                    return;
                }
                MainMod.Log(Username + " has registered under the IP " + IpAddress);
                Database.InsertRegistration(Index, hash);
                SendText(0, 255, 0, "Registered, login with /login <password>");
            }
            else
            {
                SendError("This name is already registered.");
                SendError("Please rejoin with a different character name.");
            }
        }

        public void Login(string hash)
        {
            if (Database.IsUserRegistered(Username))
            {
                int id = Database.CheckLogin(Username, hash);
                if (id == -1)
                {
                    SendError("Incorrect password!");
                    return;
                }
                IsLoggedIn = true;
                DbId = id;
                MainMod.Log(Username + " has logged in under the IP " + IpAddress);
                Group = MainMod.Groups.GetGroup(Database.GetUserGroup(Username));
                SendText(0, 255, 0, "Logged in successfully!");
            }
            else
            {
                SendError("This name is not registered.");
            }
        }

        public void ChangePassword(string hash)
        {
            if (Database.IsUserRegistered(Username) && this.IsLoggedIn)
            {
                if (hash == "")
                {
                    SendError("Change password failed. Blank password?");
                    return;
                }
                Database.ChangeUserPassword(Username, hash);
                SendText(0, 255, 0, "Password changed.");
            }
            else
            {
                SendError("You must be registered and logged in to use this command.");
            }
        }

        public void SendLoginMessage()
        {
            if (Database.IsUserRegistered(Username))
            {
                SendText("You are not logged in!");
                SendText("Log in with /login {password}");
            }
            else if (MainMod.Config.RequireRegistration)
            {
                SendText("You are not registered!");
                if (MainMod.Config.BlockRegistration)
                {
                    SendText("You must have op/mod approval to register");
                }
                else
                {
                    SendText("Register with /register {password}");
                }
            }
            if (MainMod.Config.AdvertEnabled) SendText("Server is powered by tMod ~ tMod.biz");
        }

        public void SetGroup(string group)
        {
            Console.WriteLine("[NOTICE] Setting {0} to group {1}", Username, group);
            Group = MainMod.Groups.GetGroup(group);
            Database.SetUserGroup(Username, Group.GroupName);
        }

        public void Teleport(int tx, int ty)
        {
            if (IsUsingTMod)
            {
                byte[] writeBuffer = new byte[13];
                Buffer.BlockCopy(BitConverter.GetBytes(9), 0, writeBuffer, 0, 4);
                writeBuffer[4] = 0xfe;
                Buffer.BlockCopy(BitConverter.GetBytes(tx), 0, writeBuffer, 5, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(ty), 0, writeBuffer, 9, 4);
                NetMessageMod.FlushBuffer(writeBuffer, Index);
            }
            else
            {
                if (tx < 0 || ty < 0 || tx >= MainMod.MaxTilesX || ty >= MainMod.MaxTilesY)
                {
                    Console.WriteLine("Attempt to teleport player {0} to invalid location: {1}, {2}.", Username ?? IpAddress, tx, ty);
                    return;
                }
                bool changeSpawn = false;
                int ox = MainMod.SpawnTileX;
                int oy = MainMod.SpawnTileY;
                if (SpawnX >= 0 && SpawnY >= 0)
                {
                    changeSpawn = true;
                    ox = SpawnX;
                    oy = SpawnY;
                }
                else if (OldSpawnX >= 0 && OldSpawnY >= 0)
                {
                    changeSpawn = true;
                    ox = OldSpawnX;
                    oy = OldSpawnY;
                }
                int sx = tx / 200;
                int sy = ty / 150;
                int fromX = Math.Max(0, sx - 1);
                int fromY = Math.Max(0, sy - 1);
                int toX = Math.Min(sx + 1, MainMod.MaxTilesX / 200 - 1);
                int toY = Math.Min(sy + 1, MainMod.MaxTilesY / 150 - 1);
                int sections = 0;
                for (int x = fromX; x <= toX; x++)
                    for (int y = fromY; y <= toY; y++)
                        if (!NetplayMod.ServerSock[Index].tileSection[x, y]) sections += 1;
                if (sections > 0)
                {
                    NetMessageMod.SendData(9, Index, -1, "Prepairing to teleport...", sections * 150);
                    for (int x = fromX; x <= toX; x++)
                        for (int y = fromY; y <= toY; y++)
                            if (!NetplayMod.ServerSock[Index].tileSection[x, y])
                                NetMessageMod.SendSection(Index, x, y);
                    NetMessageMod.SendData(11, Index, -1, "", fromX, fromY, toX, toY);
                }
                for (int proj = 0; proj < MainMod.Projectile.Length; ++proj)
                    if (MainMod.Projectile[proj].active && MainMod.Projectile[proj].owner == Index && (MainMod.Projectile[proj].type == 13 || MainMod.Projectile[proj].type == 32))
                    {
                        MainMod.Projectile[proj].active = false;
                        MainMod.Projectile[proj].type = 0;
                        NetMessageMod.SendData(27, -1, -1, "", proj);
                    }
                int left = 0;
                int right = -1;
                if (changeSpawn && oy > 1)
                {
                    left = Math.Max(0, ox - 4);
                    right = Math.Min(ox + 4, MainMod.MaxTilesX);
                    while (left < MainMod.MaxTilesX && MainMod.Tile[left, oy - 1].type != 79)
                        left += 1;
                    while (right > 0 && MainMod.Tile[right, oy - 1].type != 79)
                        right -= 1;
                    for (int x = left; x <= right; x++)
                    {
                        MainMod.Tile[x, oy - 1].active = false;
                        NetMessageMod.SendTileSquare(Index, x, oy - 1, 1);
                        MainMod.Tile[x, oy - 2].active = false;
                        NetMessageMod.SendTileSquare(Index, x, oy - 2, 1);
                    }
                }
                int tX = MainMod.SpawnTileX, tY = MainMod.SpawnTileY;
                MainMod.SpawnTileX = tx; MainMod.SpawnTileY = ty;
                NetMessageMod.SendData(7, Index);
                MainMod.SpawnTileX = tX; MainMod.SpawnTileY = tY;
                TeleSpawnX = tx;
                TeleSpawnY = ty;
                NetMessageMod.SendData(12, Index, -1, "", Index);
                int fx = Math.Max(0, Math.Min(MainMod.MaxTilesX - 8, tx - 4));
                int fy = Math.Max(0, Math.Min(MainMod.MaxTilesY - 8, ty - 4));
                NetMessageMod.SendTileSquare(Index, fx, fy, 7);
                if (changeSpawn && oy > 1)
                {
                    NetMessageMod.SendTileSquare(Index, ox, oy, 1);
                    if (right - left >= 0 && oy >= 2)
                        NetMessageMod.SendTileSquare(Index, left, oy - 2, right - left + 1);
                }
                NetMessageMod.SendData(7, Index);
            }
        }
    }
}