using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using LuaInterface;
using System.Threading;
using Terraria;

namespace tMod_v3
{
    public class tMod
    {
        public static Type worldGen;
        public static Type main;
        AssemblyDefinition terrariaAsm;
        ModuleDefinition module;
        ModuleDefinition tMod2;
        MemoryStream stream = new MemoryStream();

        public void writeLine(string write)
        {
            Console.WriteLine(write);
        }

        public void inject()
        {
            Load();

            module.Types.Remove(module.Types["Terraria.ProgramServer"]);
            module.Inject(tMod2.Types["Terraria.Program"]);
            terrariaAsm.EntryPoint = getMethod(module.Types["Terraria.Program"], "tMod");

            modifyMain();
            ModifyMessageBuffer();
            ModifyNetMessage();
            ModifyNetplay();

            // XeedMod
            Modify();

            Write();

            Assembly asm = System.Reflection.Assembly.Load(stream.GetBuffer());
            worldGen = asm.GetType("Terraria.WorldGen");
            main = asm.GetType("Terraria.Main");

            ItemMod.Item = asm.GetType("Terraria.Item");
            MainMod.main = asm.GetType("Terraria.Main");
            MessageBufferMod.messageBuffer = asm.GetType("Terraria.messageBuffer");
            NetMessageMod.NetMessage = asm.GetType("Terraria.NetMessage");
            NetplayMod.Netplay = asm.GetType("Terraria.Netplay");
            NPCMod.NPC = asm.GetType("Terraria.NPC");
            WorldGenMod.WorldGen = asm.GetType("Terraria.WorldGen");

            try
            {
                asm.EntryPoint.Invoke(null, new object[] { new string[0] });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Program crashed...");
            }
            
            Console.ReadKey();
        }

        private void Write()
        {
            AssemblyFactory.SaveAssembly(terrariaAsm, stream);
        }

        private void Load()
        {
            Console.WriteLine("Loading original Terraria assembly");
            tMod2 = AssemblyFactory.GetAssembly(System.Reflection.Assembly.GetExecutingAssembly().Location).MainModule;
            while (true)
            {
                try
                {
                    string Path = Environment.CurrentDirectory + @"\";
                    terrariaAsm = AssemblyFactory.GetAssembly(Path + "TerrariaServer.exe");
                    module = terrariaAsm.MainModule;
                    break;
                }
                catch
                {
                    Console.WriteLine("TerrariaServer.exe couldn't be found in the same directory as tModServer.exe");
                    Console.WriteLine("Please move tMod v3.exe into the same directory as the TerrariaServer.exe");
                    Console.ReadKey();
                }
            }
        }

        public void ModifyNetplay()
        {
            ExceptionHandler exh = new ExceptionHandler(ExceptionHandlerType.Catch);
            TypeReference exception = module.Import(typeof(Exception));
            VariableDefinition ex = new VariableDefinition(exception);
            MethodDefinition listenForClients = getMethod(module.Types["Terraria.Netplay"], "ListenForClients");
            CilWorker cil = listenForClients.Body.CilWorker;
            Instruction instr = listenForClients.Body.Instructions[listenForClients.Body.Instructions.Count - 1];
            listenForClients.Body.Variables.Add(ex);
            exh.TryStart = listenForClients.Body.Instructions[0];
            exh.CatchType = exception;
            cil.InsertAfter(instr, instr = exh.TryEnd = exh.HandlerStart = cil.Create(OpCodes.Stloc, ex));
            cil.InsertAfter(instr, instr = cil.Create(OpCodes.Ldloc, ex));
            cil.InsertAfter(instr, instr = cil.Create(OpCodes.Call, module.Import(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(object) }))));
            cil.InsertAfter(instr, instr = exh.HandlerEnd = cil.Create(OpCodes.Ret));
            listenForClients.Body.ExceptionHandlers.Add(exh);
        }

        public void modifyMain()
        {
            Console.WriteLine("Modifying Terraria.Main");
            TypeDefinition type = module.Types["Terraria.Main"];
            MethodDefinition update = getMethod(type, "Update");
            MethodReference updateMod = module.Import(typeof(MainMod).GetMethod("UpdateMod"));
            MethodDefinition startDedInput = getMethod(type, "startDedInput");
            MethodReference startDedInputMod = module.Import(typeof(MainMod).GetMethod("StartDedInputMod"));

            CilWorker cil;

            // Call UpdateMod
            cil = update.Body.CilWorker;
            cil.InsertBefore(update.Body.Instructions[0], cil.Create(OpCodes.Call, updateMod));

            // Call StartDedInputMod
            cil = startDedInput.Body.CilWorker;
            cil.InsertBefore(startDedInput.Body.Instructions[startDedInput.Body.Instructions.Count - 1], cil.Create(OpCodes.Call, startDedInputMod));
        }

        public void Modify()
        {
            Console.WriteLine("[XeedMod] Modifying Terraria.Item");
            TypeDefinition type = module.Types["Terraria.Item"];
            MethodDefinition mdef = getMethod(type, "SetDefaults", 1);
            MethodReference refer = module.Import(typeof(ItemMod).GetMethod("SetDefaultsMod"));
            CilWorker mcil = mdef.Body.CilWorker;
            Instruction tar = mdef.Body.Instructions[3];
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ldarg_1));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ldarg_0));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Call, refer));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Brfalse_S, tar));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ret));
            Console.WriteLine("[XeedMod] Modifying Terraria.NPC");
            type = module.Types["Terraria.NPC"];
            mdef = getMethod(type, "SetDefaults", 1);
            refer = module.Import(typeof(NPCMod).GetMethod("SetDefaultsMod"));
            mcil = mdef.Body.CilWorker;
            tar = mdef.Body.Instructions[4];
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ldarg_0));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ldarg_1));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Call, refer));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Brfalse_S, tar));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ret));
            mdef = getMethod(type, "NPCLoot", 0);
            refer = module.Import(typeof(NPCMod).GetMethod("NPCLootMod"));
            mcil = mdef.Body.CilWorker;
            tar = mdef.Body.Instructions[0];
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Ldarg_0));
            mcil.InsertBefore(tar, mcil.Create(OpCodes.Call, refer));
        }

        private void ModifyNetMessage()
        {
            Console.WriteLine("Modifying Terraria.NetMessage");
            TypeDefinition type = module.Types["Terraria.NetMessage"];
            MethodDefinition greetPlayer = getMethod(type, "greetPlayer");
            MethodReference greetPlayerMod = module.Import(typeof(NetMessageMod).GetMethod("GreetPlayerMod"));
            MethodDefinition SendData = getMethod(type, "SendData");
            MethodReference SendDataTrick = module.Import(typeof(NetMessageMod).GetMethod("SendDataTrick"));

            CilWorker cil = greetPlayer.Body.CilWorker;
            Instruction instr = greetPlayer.Body.Instructions[greetPlayer.Body.Instructions.Count - 1];

            cil.InsertBefore(instr, cil.Create(OpCodes.Ldarg_0));
            cil.InsertBefore(instr, cil.Create(OpCodes.Call, greetPlayerMod));

            /*cil = SendData.Body.CilWorker;
            List<Instruction> ins = new List<Instruction>();
            foreach (Instruction str in SendData.Body.Instructions)
            {
                if (str.Operand != null && str.Operand.ToString().Contains("BeginWrite"))
                {
                    ins.Add(str);
                }
            }
            foreach (Instruction str in ins)
            {
                cil.Replace(str, cil.Create(OpCodes.Callvirt, SendDataTrick));
            }*/
        }

        private void ModifyMessageBuffer()
        {
            Console.WriteLine("Modifying Terraria.messageBuffer");
            TypeDefinition type = module.Types["Terraria.messageBuffer"];
            MethodDefinition getData = getMethod(type, "GetData");
            MethodReference getDataMod = module.Import(typeof(MessageBufferMod).GetMethod("GetDataMod"));

            CilWorker cil = getData.Body.CilWorker;
            Instruction instr = getData.Body.Instructions[0];
            cil.InsertBefore(instr, cil.Create(OpCodes.Ldarg_0));
            cil.InsertBefore(instr, cil.Create(OpCodes.Ldarg_1));
            cil.InsertBefore(instr, cil.Create(OpCodes.Ldarg_2));
            cil.InsertBefore(instr, cil.Create(OpCodes.Call, getDataMod));
            cil.InsertBefore(instr, cil.Create(OpCodes.Brfalse, getData.Body.Instructions[getData.Body.Instructions.Count - 1]));
        }

        private static MethodDefinition getMethod(TypeDefinition type, string name)
        {
            for (int i = 0; i < type.Methods.Count; i++)
            {
                if (type.Methods[i].Name == name)
                {
                    return type.Methods[i];
                }
            }
            throw new Exception("Method " + name + " does not exist.");
        }
    }

    internal class CustomItem
    {
        public dynamic data = new Dictionary<dynamic, dynamic>();
        public dynamic name = "";
        internal dynamic ctex = new byte[0];
        internal dynamic shname;
        internal dynamic scope = false;
        internal dynamic iId = 0;

        internal CustomItem(dynamic shortName) { shname = shortName; }

        internal void AddField(dynamic data, dynamic value)
        {
            if (data.Length == 2)
            {
                dynamic obj = null;
                switch ((string)data[0])
                {
                    case "bool": bool bval; if (bool.TryParse(value, out bval)) obj = bval; break;
                    case "int": int ival; if (int.TryParse(value, out ival)) obj = ival; break;
                    case "float": float fval; if (float.TryParse(value, out fval)) obj = fval; break;
                    case "string": obj = value; break;
                    case "Color": byte a, r, g, b; string[] t = value.Split(','); if (t.Length == 4 && byte.TryParse(t[0], out r) && byte.TryParse(t[1], out g) && byte.TryParse(t[2], out b) && byte.TryParse(t[3], out a)) obj = new Color(r, g, b, a); break;
                }
                dynamic fi = ItemMod.Item.GetField(data[1]);
                if (fi == null) Console.WriteLine("[CustomPlug] ERROR: Field '" + data[1] + "' not found!");
                else if (fi.IsStatic) Console.WriteLine("[CustomPlug] ERROR: Field '" + data[1] + "' is static and cannot be used!");
                else if (obj == null) Console.WriteLine("[CustomPlug] ERROR: Value '" + value + "' or/and type '" + data[0] + "' is incorrect!");
                else if (fields.ContainsKey(data[1])) Console.WriteLine("[CustomPlug] ERROR: Field '" + data[1] + "' is already defined!");
                else fields.Add(data[1], obj);
            }
        }

        public void ApplyTo(dynamic item)
        {
            item.type = iId;
            item.name = name;
            foreach (KeyValuePair<dynamic, dynamic> kvp in data) ItemMod.Item.GetField(kvp.Key).SetValue(item, kvp.Value);
            item.active = true;
        }

        public dynamic GetID() { return iId; }
    }

    public static class XeedMod
    {
        private static dynamic ItemFile { get { return MainMod.Config.PluginDataDirectory + "\\ItemData.txt"; } }

        public static dynamic GetObj(dynamic list, dynamic name)
        {
            foreach (dynamic obj in list) if (String.Equals(obj.name, name)) return obj;
            return null;
        }

        static XeedMod()
        {
            MainMod.LoadConfig();
            items = new List<CustomItem>();
            if (File.Exists(ItemFile))
            {
                CustomItem ci = null;
                foreach (string line in File.ReadAllLines(ItemFile))
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        if (ci != null) items.Add(ci);
                        ci = new CustomItem(line.Remove(line.Length - 1, 1).Remove(0, 1));
                    }
                    else
                    {
                        string[] data = line.Split(new char[] { '=' }, 2);
                        if (data[0].Equals("baseId")) ci.iId = int.Parse(data[1]);
                        else if (data[0].Equals("name")) ci.name = data[1];
                        else if (data[0].Equals("scope-effect")) ci.scope = bool.Parse(data[1]);
                        else if (data[0].Equals("texture-name") && !String.IsNullOrWhiteSpace(data[1]))
                            ci.ctex = ImageToByte2(System.Drawing.Image.FromFile(MainMod.Config.PluginDataDirectory + @"\Textures\" + data[1]));
                        else if (!data[0].StartsWith("#")) ci.AddField(data[0].Split(':'), data[1]);
                    }
                if (ci != null) items.Add(ci);
            }
        }

        private static dynamic ImageToByte2(dynamic img)
        {
            dynamic byteArray = new byte[0];
            using (dynamic stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();
                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        internal static dynamic cmobs { get { return CPlugin == null ? null : CPlugin.mobs; } }

        internal static dynamic cprojs { get { return CPlugin == null ? null : CPlugin.projs; } }

        internal static dynamic GetItems()
        {
            if (CPlugin != null) return CPlugin.items;
            else return items;
        }

        internal static dynamic items;

        internal static dynamic CPlugin { get { return PluginManager.GetPlugin("CustomPlug"); } }

        internal static dynamic GetItemName(dynamic itemId)
        {
            dynamic obj2 = ItemMod.Item.GetConstructor(new Type[0]).Invoke(new object[0]);
            obj2.SetDefaults(itemId, true);
            return obj2.name;
        }
    }
}
