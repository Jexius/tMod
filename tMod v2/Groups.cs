using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace tMod_v3
{
    [Serializable]
    public class Groups
    {
        public Group Ops = new Group();
        public Group Mods = new Group();
        public Group Default = new Group();
        public Group Member = new Group();
        public List<Group> CustomGroups = new List<Group>();

        public Group GetCustomGroup(string name)
        {
            //Console.WriteLine("GetCustomGroup: {0}", name);
            // in case it's not custom, I don't know why I called it GetCustomGroup
            name = name.ToLower();
            if (name == "ops")
            {
                return Ops;
            }
            else if (name == "mods")
            {
                return Mods;
            }
            else if (name == "default")
            {
                return Default;
            }
            else if (name == "member")
            {
                return Member;
            }
            foreach (Group group in CustomGroups)
            {
                if (group.GroupName.ToLower() == name)
                {
                    return group;
                }
            }
            return Default;
        }
    }

    [Serializable]
    public class Group
    {
        public string GroupName = "Default";
        public StringCollection GroupPermissions = new StringCollection();
        public string ChatPrefix = "";
        public Rgb ChatColor = new Rgb(255, 255, 255);
        public string DerivesFrom = "Default";
        public bool CanBuild = true;
    }
}
