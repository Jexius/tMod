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
                if (MainMod.IsOp(Index))
                {
                    Group = MainMod.Groups.Ops;
                }
                else if (MainMod.IsMod(Index))
                {
                    Group = MainMod.Groups.Mods;
                }
                else
                {
                    Group = MainMod.Groups.GetCustomGroup(Database.GetUserGroup(Username));
                }
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
            Group = MainMod.Groups.GetCustomGroup(group);
            Database.SetUserGroup(Username, Group.GroupName);
        }

        public void Teleport(int x, int y)
        {
            if (IsUsingTMod)
            {
                byte[] writeBuffer = new byte[13];
                Buffer.BlockCopy(BitConverter.GetBytes(9), 0, writeBuffer, 0, 4);
                writeBuffer[4] = 0xfe;
                Buffer.BlockCopy(BitConverter.GetBytes(x), 0, writeBuffer, 5, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(y), 0, writeBuffer, 9, 4);
                NetMessageMod.FlushBuffer(writeBuffer, Index);
            }
            else
            {
                int spawnTileX = MainMod.SpawnTileX;
                int spawnTileY = MainMod.SpawnTileY;
                int oldPlayerX = MainMod.Player[Index].SpawnX;
                int oldPlayerY = MainMod.Player[Index].SpawnY;
                MainMod.SpawnTileX = x;
                MainMod.SpawnTileY = y;
                NetMessageMod.SendData(0x7, Index);
                NetMessageMod.SendTileSquare(Index, x, y, 200);
                if (MainMod.Player[Index].SpawnX >= 0 && MainMod.Player[Index].SpawnY >= 0)
                {
                    //Invalidate player spawn point
                    MainMod.Player[Index].SpawnX = x;
                    MainMod.Player[Index].SpawnY = y;
                    MainMod.Tile[x, y - 1].active = false;
                    NetMessageMod.SendTileSquare(Index, x, y - 1, 200);
                    NetMessageMod.SendData(0xc, Index, -1, "", Index, 0f, 0f, 0f);
                    MainMod.Tile[x, y - 1].active = true;
                }
                else
                {
                    NetMessageMod.SendData(0xc, Index, -1, "", Index, 0f, 0f, 0f);
                }
                MainMod.SpawnTileX = spawnTileX;
                MainMod.SpawnTileY = spawnTileY;
                MainMod.Player[Index].SpawnX = oldPlayerX;
                MainMod.Player[Index].SpawnY = oldPlayerY;
                NetMessageMod.SendData(0x7, Index);
            }

        }
    }
}
