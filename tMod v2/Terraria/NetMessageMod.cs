using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using tMod_v3;
using System.ComponentModel;
using System.Reflection;

namespace Terraria
{
    public class NetMessageMod
    {
        public static Type NetMessage;

        private static BackgroundWorker bgw = new BackgroundWorker();
        private static List<object[]> queue = new List<object[]>();

        static NetMessageMod() { bgw.DoWork += new DoWorkEventHandler(bgw_DoWork); bgw.WorkerSupportsCancellation = true; bgw.RunWorkerAsync(); }

        private static void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bgw.CancellationPending)
            {
                if (queue.Count > 0)
                {
                    try
                    {
                        MethodBase meth = (MethodBase)queue[0][0];
                        meth.Invoke(null, (object[])queue[0][1]);
                    }
                    catch (Exception ex) { Console.WriteLine(DateTime.Now + " ASYNC"); Console.WriteLine(ex); }
                    queue.RemoveAt(0);
                }
                else System.Threading.Thread.Sleep(5);
            }
        }

        static dynamic[] Buffer
        {
            get
            {
                return (dynamic[])NetMessage.GetField("buffer").GetValue(null);
            }
            set
            {
                NetMessage.GetField("buffer").SetValue(null, value);
            }
        }

        public static void FireAsync(System.Reflection.MethodBase method, object[] args)
        {
            queue.Add(new object[] { method, args });
        }

        public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, string d = "", int e = 0, float f = 0f, float g = 0f, float h = 0f, int i = 0)
        {
            if (MainMod.Config.AsyncMode) FireAsync(NetMessage.GetMethod("SendData"), new object[] { msgType, remoteClient, ignoreClient, d, e, f, g, h, i });
            else NetMessage.GetMethod("SendData").Invoke(null, new object[] { msgType, remoteClient, ignoreClient, d, e, f, g, h, i });
        }

        public static void SendSection(int whoAmi, int sectionX, int sectionY)
        {
            if (MainMod.Config.AsyncMode) FireAsync(NetMessage.GetMethod("SendSection"), new object[] { whoAmi, sectionX, sectionY });
            else NetMessage.GetMethod("SendSection").Invoke(null, new object[] { whoAmi, sectionX, sectionY });
        }

        public static void SendTileSquare(int plr, int x, int y, int size)
        {
            if (MainMod.Config.AsyncMode) FireAsync(NetMessage.GetMethod("SendTileSquare"), new object[] { plr, x, y, size });
            else try
            {
                NetMessage.GetMethod("SendTileSquare").Invoke(null, new object[] { plr, x, y, size });
            }
            catch
            {
                Console.WriteLine("Failed to SendTileSquare. Too large?");
            }
        }

        public static void SyncPlayers()
        {
            if (MainMod.Config.AsyncMode) FireAsync(NetMessage.GetMethod("syncPlayers"), new object[0]);
            else NetMessage.GetMethod("syncPlayers").Invoke(null, new object[0]);
        }

        public static void BroadcastMessage(string text)
        {
            BroadcastMessage(175, 75, 255, text);
        }

        public static void BroadcastMessage(byte r, byte g, byte b, string text, int from = 0xff)
        {
            MainMod.Notice(text);
            NetMessageMod.SendData(0x19, -1, -1, text, from, r, g, b);
        }

        public static void FlushBuffer(byte[] writeBuffer, int remoteClient = -1, int ignoreClient = -1)
        {
            if (MainMod.NetMode == 1)
            {
                if (NetplayMod.ClientSock.tcpClient.Connected)
                {
                    try
                    {
                        NetplayMod.ClientSock.networkStream.BeginWrite(writeBuffer, 0, writeBuffer.Length, new Callback().ClientWriteCallback, NetplayMod.ClientSock.networkStream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (remoteClient < 0)
            {
                for (int i = 0; i < 0x100; i++)
                {
                    if (((i != ignoreClient) && (NetMessageMod.Buffer[i].broadcast || ((NetplayMod.ServerSock[i].state >= 3) && (writeBuffer[4] == 10)))) && NetplayMod.ServerSock[i].tcpClient.Connected)
                    {
                        try
                        {
                            NetplayMod.ServerSock[i].networkStream.BeginWrite(writeBuffer, 0, writeBuffer.Length, new Callback(i).ServerWriteCallback, NetplayMod.ServerSock[i].networkStream);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("A net message exception occured");
                            using (StreamWriter writer = new StreamWriter("NetMessageException.txt", true))
                            {
                                writer.WriteLine(DateTime.Now);
                                writer.WriteLine(exception);
                                writer.WriteLine("");
                            }
                        }
                    }
                }
            }
            else if (NetplayMod.ServerSock[remoteClient].tcpClient.Connected)
            {
                try
                {
                    NetplayMod.ServerSock[remoteClient].networkStream.BeginWrite(writeBuffer, 0, writeBuffer.Length, new Callback(remoteClient).ServerWriteCallback, NetplayMod.ServerSock[remoteClient].networkStream);
                }
                catch (Exception exception)
                {
                    MainMod.Log("A net message exception occured");
                    using (StreamWriter writer = new StreamWriter("NetMessageException.txt", true))
                    {
                        writer.WriteLine(DateTime.Now);
                        writer.WriteLine(exception);
                        writer.WriteLine("");
                    }
                }
            }
            else
            {
                //Console.WriteLine("Client {0} is already disconnected", remoteClient);
            }
        }

        public static void GreetPlayerMod(int id)
        {
            Session.Sessions[id].SendLoginMessage();
        }

        private class Callback
        {
            private int Index;

            public AsyncCallback ClientWriteCallback { get; private set; }
            public AsyncCallback ServerWriteCallback { get; private set; }

            public Callback(int index)
                : this()
            {
                Index = index;
            }

            public Callback()
            {
                ClientWriteCallback = new AsyncCallback(ClientWriteCallbackProc);
                ServerWriteCallback = new AsyncCallback(ServerWriteCallbackProc);
            }

            public void ClientWriteCallbackProc(IAsyncResult ar)
            {
                NetplayMod.ClientSock.ClientWriteCallBack(ar);
            }

            public void ServerWriteCallbackProc(IAsyncResult ar)
            {
                NetplayMod.ServerSock[Index].ServerWriteCallBack(ar);
            }
        }
    }
}