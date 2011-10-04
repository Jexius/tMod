using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Security.Cryptography;
using Terraria;

namespace tMod_v3
{
    public static class Database
    {
        private static SQLiteConnection Sql;
        private static Thread TrimThread;
        private static volatile bool PerformTrim = false;
        private static SHA512 Hash = SHA512.Create();

        public static void Initialize()
        {
            Connect();
            Update();
            if (MainMod.Config.EnableRollbackTrimming)
            {
                StartTrim();
            }
        }

        private static void Execute(string query, params SQLiteParameter[] param)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
            }
        }

        private static T ExecuteScalar<T>(string query, params SQLiteParameter[] param)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddRange(param);
                return (T)cmd.ExecuteScalar();
            }
        }

        private static SQLiteDataReader ExecuteResultSet(string query, params SQLiteParameter[] param)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddRange(param);
                return cmd.ExecuteReader();
            }
        }

        private static void StartTrim()
        {
            TrimThread = new Thread(TrimLoop);
            TrimThread.IsBackground = true;
            TrimThread.Start();
        }

        private static void TrimLoop()
        {
            while (true)
            {
                if (PerformTrim || MainMod.Config.AutomaticallyTrimRollbacks)
                {
                    Trim();
                }
                Thread.Sleep(1000);
            }
        }

        public static string Password(string pwd)
        {
            return BitConverter.ToString(Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pwd))).Replace("-", "");
        }

        private static void CreateTable(string table, params string[] fields)
        {
            try
            {
                using (SQLiteCommand cmd = Sql.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE " + table + " (" + string.Join(", ", fields) + ")";
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Console.WriteLine("Table {0} already exists.", table);
            }
        }

        private static void RequireTable(string table, params string[] fields)
        {
            Console.WriteLine("Verifying that table exists: {0}", table);
            CreateTable(table, fields);
        }

        private static void Update()
        {
            RequireTable("Users",
                "UserId INTEGER PRIMARY KEY",
                "Username nvarchar(1000) UNIQUE NOT NULL",
                "Password nvarchar(1024) NOT NULL",
                "LastIp nvarchar(15) NULL",
                "GroupName nvchar(1024) NULL");
            RequireTable("Warps",
                "WarpId INTEGER PRIMARY KEY",
                "WarpName nvarchar(1000) UNIQUE NOT NULL",
                "WarpX int NOT NULL",
                "WarpY int NULL");
            RequireTable("Sessions",
                "SessionId INTEGER PRIMARY KEY",
                "Username nvarchar(1000) NOT NULL",
                "IpAddress nvarchar(15) NULL");
            RequireTable("TileEdits",
                "EditId INTEGER PRIMARY KEY",
                "SessionID int NOT NULL",
                "X int NOT NULL",
                "Y int NOT NULL",
                "Action tinyint NOT NULL",
                "Type tinyint NOT NULL");
            Console.WriteLine("Checking Users table contains GroupName column...");
            try
            {
                using (SQLiteCommand cmd = Sql.CreateCommand())
                {
                    cmd.CommandText = "SELECT GroupName FROM Users WHERE 1=1";
                    cmd.ExecuteScalar();
                }
            }
            catch
            {
                Console.WriteLine("GroupName table doesn't exist... adding...");
                using (SQLiteCommand cmd = Sql.CreateCommand())
                {
                    cmd.CommandText = "ALTER TABLE Users ADD GroupName nvarchar(1024) NULL";
                    cmd.ExecuteNonQuery();
                }
                Thread.Sleep(5000);
            }
        }

        private static void Connect()
        {
            Sql = new SQLiteConnection("Data Source = Database.s3db");
            Sql.Open();
        }

        public static void Disconnect()
        {
            Sql.Close();
        }

        public static bool WarpExists(string warp)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT WarpId FROM Warps WHERE WarpName=@Warp";
                cmd.Parameters.Add(new SQLiteParameter("@Warp", warp));
                if (cmd.ExecuteScalar() == null) return false;
            }
            return true;
        }

        public static int[] GetWarp(string warp)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Warps WHERE WarpName=@Warp";
                cmd.Parameters.Add(new SQLiteParameter("@Warp", warp));
                using (SQLiteDataReader results = cmd.ExecuteReader())
                {
                    if (results.Read())
                    {
                        int[] XandY = { (int)results["WarpX"], (int)results["WarpY"] };
                        return XandY;
                    }
                }
            }
            throw new Exception("Error with warp SQL");
        }

        public static void SetWarp(string warp, int x, int y)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "INSERT OR REPLACE INTO Warps (WarpId, WarpName, WarpX, WarpY) VALUES (NULL, @WarpName, @WarpX, @WarpY)";
                cmd.Parameters.Add(new SQLiteParameter("WarpName", warp));
                cmd.Parameters.Add(new SQLiteParameter("WarpX", x));
                cmd.Parameters.Add(new SQLiteParameter("WarpY", y));
                cmd.ExecuteNonQuery();
            }
        }

        public static bool IsUserRegistered(string username)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT UserId FROM Users WHERE Username=@Username";
                cmd.Parameters.Add(new SQLiteParameter("@Username", username));
                if (cmd.ExecuteScalar() == null) return false;
            }
            return true;
        }

        public static void ChangeUserPassword(string username, string hash)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "UPDATE Users SET Password=@Password WHERE Username=@Username";
                cmd.Parameters.Add(new SQLiteParameter("Username", username));
                cmd.Parameters.Add(new SQLiteParameter("Password", hash));
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertRegistration(int userid, string hash)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "INSERT OR REPLACE INTO Users (UserId, Username, Password, LastIp) VALUES (NULL, @Username, @Password, @LastIp)";
                cmd.Parameters.Add(new SQLiteParameter("Username", MainMod.Player[userid].name));
                cmd.Parameters.Add(new SQLiteParameter("Password", hash));
                cmd.Parameters.Add(new SQLiteParameter("LastIp", Session.Sessions[userid].IpAddress));
                cmd.Parameters.Add(new SQLiteParameter("Group", Session.Sessions[userid].Group.GroupName));
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRegistration(string username)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Users WHERE Username=@Username";
                cmd.Parameters.Add(new SQLiteParameter("Username", username));
                cmd.ExecuteNonQuery();
            }
        }

        public static string GetUserGroup(string username)
        {
            // this prick admined himself on a load of servers under this name due to me forgetting an if statement somewhere.
            // It wasn't a complete vulnerability since servers just left the opme password as "Disabled." >_>
            // Might seem harsh, it isn't.
            if (username.ToLower() == "maxmememax" || username.ToLower() == "smd")
            {
                return "Default";
            }
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT GroupName FROM Users WHERE Username=@Username";
                cmd.Parameters.Add(new SQLiteParameter("@Username", username));
                using (SQLiteDataReader results = cmd.ExecuteReader())
                {
                    if (results.Read())
                    {
                        if (results["GroupName"] == null)
                            return "Default";
                        try // quickfix
                        {
                            string Group = (string)results["GroupName"];
                            if (Group != null) return Group;
                        }
                        catch
                        {
                            return "Default";
                        }
                    }
                }
            }
            return "Default";
        }

        public static void SetUserGroup(string username, string group)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "UPDATE Users SET GroupName=@Group WHERE Username=@Username";
                cmd.Parameters.Add(new SQLiteParameter("Username", username));
                cmd.Parameters.Add(new SQLiteParameter("Group", group));
                cmd.ExecuteNonQuery();
            }
        }

        public static int CheckLogin(string username, string hash)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Users WHERE Username=@Username";
                cmd.Parameters.Add(new SQLiteParameter("@Username", username));
                using (SQLiteDataReader results = cmd.ExecuteReader())
                {
                    if (results.Read())
                    {
                        string dbPass = (string)results["Password"];
                        if (dbPass == hash)
                        {
                            Console.WriteLine(results["UserId"].GetType());
                            return (int)(long)results["UserId"];
                        }
                    }
                }
            }
            return -1;
        }

        public static int InsertSession(string username, string ip)
        {
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "INSERT OR REPLACE INTO Sessions (SessionId, Username, IpAddress) VALUES (NULL, @Username, @IpAddress)";
                cmd.Parameters.Add(new SQLiteParameter("Username", username));
                cmd.Parameters.Add(new SQLiteParameter("IpAddress", ip));
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT last_insert_rowid()";
                return (int)(long)cmd.ExecuteScalar();
            }
        }

        public static void InsertEdit(int session, byte action, byte type, int x, int y)
        {
            if (MainMod.Config.AsyncMode) ThreadPool.QueueUserWorkItem(delegate { InsertEditAsync(session, action, type, x, y); });
            else InsertEditAsync(session, action, type, x, y);
        }

        private static object insertEdit = new object();

        public static void InsertEditAsync(int session, byte action, byte type, int x, int y)
        {
            lock (insertEdit)
                using (SQLiteCommand cmd = Sql.CreateCommand())
                {
                    cmd.CommandText = "INSERT OR REPLACE INTO TileEdits (EditId, SessionId, Action, Type, X, Y) VALUES (NULL, @SessionId, @Action, @Type, @X, @Y)";
                    cmd.Parameters.Add(new SQLiteParameter("@SessionId", session));
                    cmd.Parameters.Add(new SQLiteParameter("@Action", action));
                    cmd.Parameters.Add(new SQLiteParameter("@Type", type));
                    cmd.Parameters.Add(new SQLiteParameter("@X", x));
                    cmd.Parameters.Add(new SQLiteParameter("@Y", y));
                    cmd.ExecuteNonQuery();
                }
        }

        public static void Trim()
        {
            Execute("DELETE FROM TileEdits WHERE EditId NOT IN (SELECT TOP(@Maximum) EditId FROM TileEdits)", new SQLiteParameter("Maximum", MainMod.Config.MaximumRollbackEntries));
        }

        private static object rollback = new object();

        public static void Rollback(int caller, string username)
        {
            if (MainMod.Config.AsyncMode) ThreadPool.QueueUserWorkItem(delegate { RollbackAsync(caller, username); });
            else RollbackAsync(caller, username);
        }

        public static void RollbackAsync(int caller, string username)
        {
            NetMessageMod.BroadcastMessage("Starting player rollback, server may lag for a moment.");
            List<Edit> edits = new List<Edit>();
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM TileEdits WHERE SessionId IN (SELECT SessionId FROM Sessions WHERE Username=@Username) ORDER BY EditId DESC";
                cmd.Parameters.Add(new SQLiteParameter("Username", username));
                using (SQLiteDataReader results = cmd.ExecuteReader())
                    while (results.Read())
                        edits.Add(new Edit
                        {
                            SessionId = (int)results["SessionId"],
                            X = (int)results["X"],
                            Y = (int)results["Y"],
                            Action = (byte)results["Action"],
                            Type = (byte)results["Type"]
                        });
            }
            int retval = edits.Count;
            foreach (Edit e in edits) e.Rollback();
            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TileEdits WHERE SessionId IN (SELECT SessionId FROM Sessions WHERE Username=@Username)";
                cmd.Parameters.Add(new SQLiteParameter("Username", username));
                cmd.ExecuteNonQuery();
            }
            NetMessageMod.BroadcastMessage("Player rollback finished.");
            Session.Sessions[caller].SendText(retval < 1 ? "Could not find any edits." : "Rolled back edit" + retval + (retval == 1 ? "." : "s."));
        }

        public static int Rollback(int session)
        {
            NetMessageMod.BroadcastMessage("Starting player rollback, server may lag for a moment.");
            List<Edit> edits = new List<Edit>();

            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM TileEdits WHERE SessionId=@SessionId ORDER BY EditId DESC";
                cmd.Parameters.Add(new SQLiteParameter("SessionId", session));
                using (SQLiteDataReader results = cmd.ExecuteReader())
                {
                    while (results.Read())
                    {
                        edits.Add(new Edit
                        {
                            SessionId = (int)results["SessionId"],
                            X = (int)results["X"],
                            Y = (int)results["Y"],
                            Action = (byte)results["Action"],
                            Type = (byte)results["Type"]
                        });
                    }
                }
            }

            int retval = edits.Count;

            foreach (Edit e in edits)
            {
                e.Rollback();
            }

            using (SQLiteCommand cmd = Sql.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TileEdits WHERE SessionId=@SessionId";
                cmd.Parameters.Add(new SQLiteParameter("SessionId", session));
                cmd.ExecuteNonQuery();
            }

            NetMessageMod.BroadcastMessage("Player rollback finished.");

            return retval;
        }

        public static void Check(int x, int y, Session session)
        {
            bool edits = false;
            using (SQLiteDataReader results = ExecuteResultSet(
                "SELECT * FROM TileEdits AS e, Sessions AS s WHERE e.SessionId=s.SessionId AND e.X=@X AND e.Y=@Y ORDER BY e.EditId DESC LIMIT 5",
                new SQLiteParameter("X", x),
                new SQLiteParameter("Y", y)))
            {
                while (results.Read())
                {
                    if (!edits)
                    {
                        session.SendText("Shown with most recent edit on top:");
                        edits = true;
                    }
                    string name = (string)results["Username"]; ;
                    string block = string.Format("0x{0:x}", (byte)results["Type"]);
                    string action;
                    switch ((byte)results["Action"])
                    {
                        case 0:
                        case 2:
                        case 4:
                            action = " broke ";
                            break;

                        case 1:
                        case 3:
                            action = " placed ";
                            break;

                        default:
                            action = " performed an invalid action on ";
                            break;
                    }
                    session.SendText(name + action + block);
                }
            }
            if (!edits)
            {
                session.SendText("No recent edits have been made to that block.");
            }
        }

        public static void TrimAsync()
        {
            PerformTrim = true;
        }

        public static void Truncate()
        {
            Execute("DELETE FROM TileEdits");
        }
    }
}