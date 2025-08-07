using System;
using System.Windows.Forms;
using System.Collections.Generic;

using CITray.Controllers;
using CITray.Api.UI;

namespace CITray.UI.Options
{
    internal partial class OptionsTreeControl : UserControl
    {
        private class SectionTag : IDisposable
        {
            private OptionsSection section = null;
            private BaseOptionsPanel panel = null;

            public SectionTag(OptionsSection optionsSection)
            {
                section = optionsSection;
            }

            public OptionsSection Section
            {
                get { return section; }
            }

            public Func<BaseOptionsPanel> PanelBuilder { get; set; }

            public BaseOptionsPanel Panel
            {
                get
                {
                    if (panel == null)
                    {
                        try
                        {
                            panel = section.PanelBuilder();
                        }
                        catch (Exception ex)
                        {
                            panel = new ErrorPanel() { Exception = ex };
                        }

                        panel.Dock = DockStyle.Fill;
                    }

                    return panel;
                }
            }

            #region IDisposable Members

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                if (panel != null && !panel.IsDisposed)
                {
                    panel.Dispose();
                    panel = null;
                }
            }

            #endregion
        }

        private IServiceProvider services = null;
        private IOptionsController controller = null;
        private List<SectionTag> tags = new List<SectionTag>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsTreeControl"/> class.
        /// </summary>
        public OptionsTreeControl()
        {
            InitializeComponent();
        }

        public event EventHandler<OptionsSectionSelectedEventArgs> SectionSelected;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            
            // bind events
            optionsTree.NodeMouseClick += (s, ev) => OnNodeClicked(ev.Node);
            
            // expand the 1st level
            foreach (TreeNode node in optionsTree.Nodes) node.Expand();
            
            // determine, then raise event for the 1st selected node
            bool finished = false;
            var selectedNode = optionsTree.Nodes[0];
            while (!finished)
            {
                if (selectedNode.Nodes.Count > 0)
                    selectedNode = selectedNode.Nodes[0];
                else finished = true;
            }

            optionsTree.SelectedNode = selectedNode;
            OnNodeClicked(selectedNode);

            // Don't forget to dispose the options panels
            base.Disposed += (s, _) => DisposeTags();
        }

        /// <summary>
        /// Initializes this instance with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public void Initialize(IServiceProvider serviceProvider)
        {
            services = serviceProvider ?? This.Services;
            var controller = services.GetService<IOptionsController>(true);
            foreach (var section in controller.TopLevelSections)
                AddSection(section, null);            
        }

        private TreeNode AddSection(OptionsSection section, TreeNode parent)
        {
            var tag = new SectionTag(section);
            tags.Add(tag);
            var node = new TreeNode(section.DisplayName);
            node.Tag = tag;

            if (parent == null)
                optionsTree.Nodes.Add(node);
            else parent.Nodes.Add(node);

            foreach (var childSection in section.Sections)
                AddSection(childSection, node);

            return node;
        }

        private void OnNodeClicked(TreeNode node)
        {
            var tag = (SectionTag)node.Tag;
            if (SectionSelected != null) SectionSelected(this,
                new OptionsSectionSelectedEventArgs(tag.Section, tag.Panel));
        }

        private void DisposeTags()
        {
            foreach (var tag in tags) tag.Dispose();
        }
    }
}
