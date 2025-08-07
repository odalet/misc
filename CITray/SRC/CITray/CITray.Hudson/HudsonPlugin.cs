using System;
using System.ComponentModel;

using CITray.Api;

namespace CITray.Hudson
{
    [DisplayName("Hudson"), Description("Hudson plugin for CITray")]
    public class HudsonPlugin : IPlugin
    {
        public Type ServerType
        {
            get { return typeof(Server); }
        }
    }
}
