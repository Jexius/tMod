using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace tMod_v3
{
    public class PropertiesFile : Dictionary<string, string>
    {
        string name;
        List<string> properties = new List<string>();
        public PropertiesFile(string name)
        {
            this.name = name;
        }

        public void registerProperty(string propertyName, string defaultValue = "false")
        {
            properties.Add(propertyName + Environment.NewLine + defaultValue);
        }

        public int getInteger(string propertyName)
        {
            int ret = 0;
            if (!int.TryParse(this[propertyName], out ret))
            {
                ret = -1;
            }
            return ret;
        }

        public bool getBoolean(string propertyName)
        {
            bool ret = false;
            if (!bool.TryParse(this[propertyName], out ret))
            {
                ret = false;
            }
            return ret;
        }

        public double getDouble(string propertyName)
        {
            double ret = 0;
            if (!double.TryParse(this[propertyName], out ret))
            {
                ret = -1;
            }
            return ret;
        }

        public void loadProperties()
        {
            if (!File.Exists(name + ".properties"))
            {
                File.Create(name + ".properties");
            }
            foreach (string row in File.ReadAllLines(name + ".properties"))
            {
                if (row.IndexOf('=') > -1 && row.Substring(0, 1) != "#")
                {
                    string[] split = row.Split('=');
                    split[1] = split[1].Split('#')[0];
                    if (split[0] != "" && split[1] != "")
                    {
                        this.Add(split[0], split[1]);
                    }
                    else if (split[0] != "")
                    {
                        this.Add(split[0], "");
                    }
                }
            }
            if (!File.Exists(name + ".properties"))
            {
                File.Create(name + ".properties").Close();
                Console.WriteLine("Generated {0}", name + ".properties");
            }
            StreamWriter writer = File.AppendText(name + ".properties");
            foreach (string str in properties)
            {
                string[] split = str.Split(Environment.NewLine.ToCharArray());
                if (!this.ContainsKey(split[0]))
                {
                    Console.WriteLine("Property: " + split[0] + " missing in file " + name + ".properties! Default set!");
                    this.Add(split[0], split[1]);
                    writer.WriteLine(split[0] + "=" + split[1]);
                }
            }
            writer.Close();
        }
    }
}
