using System;
using System.Collections.Generic;

using CITray.Api.UI;

namespace CITray.UI.Options
{
    internal class OptionsSection
    {
        private List<OptionsSection> sections = new List<OptionsSection>();
        private Func<BaseOptionsPanel> panelBuilder = null;

        public OptionsSection(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }

        public Func<BaseOptionsPanel> PanelBuilder 
        {
            get
            {
                if (panelBuilder == null) 
                    return () => new EmptyPanel();
                return panelBuilder;
            }
            set { panelBuilder = value; }
        }

        public IEnumerable<OptionsSection> Sections
        {
            get { return sections; }
        }

        public void AddChildSection(OptionsSection section)
        {
            if (section == null) throw new ArgumentNullException("section");
            sections.Add(section);
        }
    }
}
