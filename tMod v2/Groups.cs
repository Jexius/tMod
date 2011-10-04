using System;
using System.Collections.Generic;
using Terraria;
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

        public Group GetGroup(string name)
        {
            name = name.ToLower();
            if (name.Equals("ops")) return Ops;
            else if (name.Equals("mods")) return Mods;
            else if (name.Equals("default")) return Default;
            else if (name.Equals("member")) return Member;
            foreach (Group group in CustomGroups)
                if (group.GroupName.ToLower().Equals(name)) return group;
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

        public bool HasPermission(string cmd) { return GroupPermissions.Contains(cmd) || GroupPermissions.Contains("*") || this != MainMod.Groups.Default && MainMod.Groups.GetGroup(DerivesFrom).HasPermission(cmd); }
    }
}