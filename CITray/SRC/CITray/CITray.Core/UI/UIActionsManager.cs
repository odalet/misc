/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */

using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace CITray.Core.UI
{    
    [ProvideProperty("Action", typeof(Component)), ToolboxItemFilter("System.Windows.Forms"),
    ToolboxBitmap(typeof(UIAction), "UIActionsManager.bmp")]
    public class UIActionsManager: Component, IExtenderProvider, ISupportInitialize
    {
        private ContainerControl containerControl = null;
        private Dictionary<Type, UIActionTargetDescriptor> typesDescription = null;
        private Dictionary<Component, UIAction> targets = null;
        private UIActionCollection actions = null;        
        private bool enabled = true;
        private bool initializing = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIActionsManager"/> class.
        /// </summary>
        public UIActionsManager()
        {
            actions = new UIActionCollection(this);
            targets = new Dictionary<Component, UIAction>();
            typesDescription = new Dictionary<Type, UIActionTargetDescriptor>();

            if (!DesignMode) Application.Idle += (s, e) => OnUpdate(EventArgs.Empty); 
        }

        /// <summary>
        /// Occurs when [update].
        /// </summary>
        public event EventHandler Update;

        #region Properties

        [DefaultValue(true)]
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    RefreshActions();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public UIActionCollection Actions { get { return actions; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary<Type, UIActionTargetDescriptor> TypesDescription { get { return typesDescription; } }

        /// <summary>
        /// Gets or sets the container control.
        /// </summary>
        /// <value>The container control.</value>
        public ContainerControl ContainerControl
        {
            get { return containerControl; } set { SetContainerControl(value); }
        }

        [Browsable(false)]
        public Control ActiveControl { get { return GetActiveControl(); } }

        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value != null)
                {
                    var host = value.GetService<IDesignerHost>();
                    if (host != null)
                    {
                        var component = host.RootComponent;
                        if (component is ContainerControl)
                            SetContainerControl((ContainerControl)component);
                    }
                }
            }
        }

        #endregion                
        
        #region Public methods

        [DefaultValue(null)]
        public UIAction GetAction(Component extendee)
        {
            if (targets.ContainsKey(extendee)) return targets[extendee];
            else return null;
        }

        public void SetAction(Component extendee, UIAction action)
        {
            if (!initializing)
            {
                if (extendee == null) throw new ArgumentNullException("extendee");
                if (action != null && action.ActionList != this) 
                    throw new ArgumentException("The Action you selected is owned by another ActionList");
            }

            if (targets.ContainsKey(extendee))
            {
                targets[extendee].InternalRemoveTarget(extendee);
                targets.Remove(extendee);
            }

            if (action != null)
            {
                if (!typesDescription.ContainsKey(extendee.GetType())) typesDescription.Add(
                    extendee.GetType(), new UIActionTargetDescriptor(extendee.GetType()));

                targets.Add(extendee, action);
                action.InternalAddTarget(extendee);
            }
        } 

        #endregion

        #region Protected virtual methods

        /// <summary>
        /// Raises the <see cref="E:Update"/> event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnUpdate(EventArgs eventArgs)
        {
            if (Update != null) Update(this, eventArgs);
            foreach (UIAction action in actions) action.DoUpdate();
        }

        /// <summary>
        /// Gets the types of supported extendable components.
        /// </summary>
        /// <returns>An array of types.</returns>
        protected virtual Type[] GetSupportedTypes()
        {
            return new Type[] {typeof(ButtonBase), typeof(ToolStripButton),
                typeof(ToolStripMenuItem), typeof(ToolBarButton), typeof(MenuItem)};
        }
        #endregion

        #region Méthodes privées

        private void RefreshActions()
        {
            if (!DesignMode)
            {
                foreach (UIAction action in actions) action.RefreshEnabledCheckState();
            }
        }

        private void CheckInternalCollections()
        {
            foreach (UIAction action in targets.Values)
            {
                if (!actions.Contains(action) || (action.ActionList != this))
                    throw new InvalidOperationException(
                        "Action owned by another action list or invalid Action.ActionList");
            }
        }

        private Control GetActiveControl() { return GetActiveControl(containerControl); }
        
        private Control GetActiveControl(ContainerControl container)
        {
            if (container == null) return null;
            else if (container.ActiveControl is ContainerControl)
                return GetActiveControl((ContainerControl)container.ActiveControl);
            else return container.ActiveControl;
        }

        private void SetContainerControl(ContainerControl container)
        {
            if (containerControl != container)
            {
                containerControl = container;
                if (!DesignMode)
                {
                    Form f = containerControl as Form;
                    if (f != null)
                    {
                        f.KeyPreview = true;
                        f.KeyDown += (s, ke) =>
                            actions.Where(a => a.ShortcutKeys == ke.KeyData).Do(a => a.RunShortcut());
                    }
                }
            }
        }

        #endregion

        #region IExtenderProvider Members

        /// <summary>
        /// Specifies whether this object can provide its extender properties to the specified object.
        /// </summary>
        /// <param name="extendee">The <see cref="T:System.Object"/> to receive the extender properties.</param>
        /// <returns>
        /// true if this object can provide extender properties to the specified object; otherwise, false.
        /// </returns>
        bool IExtenderProvider.CanExtend(object extendee)
        {
            var targetType = extendee.GetType();
            return GetSupportedTypes().FirstOrDefault(
                t => t.IsAssignableFrom(targetType)) != null;
        }

        
        #endregion

        #region ISupportInitialize Members

        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public void BeginInit() { initializing = true; }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public void EndInit() 
        {
            initializing = false;
            CheckInternalCollections();
            RefreshActions();
        }        

        #endregion
    }
}
