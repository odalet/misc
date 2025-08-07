using System;
using CITray.Api.UI;

namespace CITray.UI.Options
{
    internal class OptionsSectionSelectedEventArgs : EventArgs
    {
        public OptionsSectionSelectedEventArgs(OptionsSection section, BaseOptionsPanel panel)
        {
            Section = section;
            Panel = panel;
        }

        public OptionsSection Section
        {
            get;
            private set;
        }

        public BaseOptionsPanel Panel
        {
            get;
            private set;
        }
    }
}
