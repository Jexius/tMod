using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tMod_v3
{
    public abstract class Plugin
    {
        public abstract string Name { get; }
        public abstract string Author { get; }
        public abstract Version Version { get; }

        public event EventHandler Loaded;
        public event EventHandler Unloaded;
        public event EventHandler Reloaded;

        protected internal virtual void OnLoaded(object sender, EventArgs e)
        {
            if (Loaded != null)
            {
                Loaded.Invoke(sender, e);
            }
        }

        protected internal void OnUnloaded(object sender, EventArgs e)
        {
            if (Unloaded != null)
            {
                Unloaded.Invoke(sender, e);
            }
        }

        public void Reload()
        {
            OnReloaded(this, new EventArgs());
        }

        protected virtual void OnReloaded(object sender, EventArgs e)
        {
            if (Reloaded != null)
            {
                Reloaded.Invoke(sender, e);
            }
        }
    }
}
