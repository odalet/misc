/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace CITray.Core.UI
{
    [ToolboxBitmap(typeof(UIAction), "UIAction.bmp"), DefaultEvent("Run")]
    public class UIAction : Component
    {
        private class UpdatablePropertyAttribute : Attribute { }

        protected enum ActionWorkingState : byte { Listening, Driving }

        private ActionWorkingState workingState = ActionWorkingState.Listening;
        private UIActionsManager actionList = null;
        private List<Component> targets = null;

        private CheckState checkState = CheckState.Unchecked;
        private bool enabled = true;
        private bool checkOnClick = false;
        private Keys shortcutKeys = Keys.None;
        private bool visible = true;
        private bool interceptingCheckStateChanged = false;

        private EventHandler clickEventHandler;
        private EventHandler checkStateChangedEventHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIAction"/> class.
        /// </summary>
        public UIAction()
        {
            targets = new List<Component>();

            clickEventHandler = new EventHandler(HandleClick);                
            checkStateChangedEventHandler = new EventHandler(HandleCheckStateChanged);
        }

        #region Events

        /// <summary>
        /// Occurs when this action's check state has changed.
        /// </summary>
        public event EventHandler CheckStateChanged;

        /// <summary>
        /// Occurs before running this action (cancellable).
        /// </summary>
        public event CancelEventHandler BeforeRun;

        /// <summary>
        /// Occurs when this action is about to run.
        /// </summary>
        public event EventHandler Run;

        /// <summary>
        /// Occurs after this action has run.
        /// </summary>
        public event EventHandler AfterRun;
        
        public event EventHandler Update;

        #endregion

        #region Properties

        [DefaultValue(false)]
        public bool Checked
        {
            get { return (this.checkState != CheckState.Unchecked); }
            set
            {
                if (value != Checked)
                    CheckState = value ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        [DefaultValue(CheckState.Unchecked), UpdatableProperty]
        public CheckState CheckState
        {
            get { return checkState; }
            set
            {
                if (checkState != value)
                {
                    checkState = value;
                    UpdateAllTargets("CheckState", value);
                    if (CheckStateChanged != null) CheckStateChanged(this, EventArgs.Empty);
                }
            }
        }

        [DefaultValue(true), UpdatableProperty]
        public bool Enabled
        {
            get
            {
                if (ActionList != null) return enabled && ActionList.Enabled;
                else return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    UpdateAllTargets("Enabled", value);
                }
            }
        }

        [DefaultValue(false), UpdatableProperty]
        public bool CheckOnClick
        {
            get { return checkOnClick; }
            set
            {
                if (checkOnClick != value)
                {
                    checkOnClick = value;
                    UpdateAllTargets("CheckOnClick", value);
                }
            }
        }

        [DefaultValue(Keys.None), UpdatableProperty, Localizable(true)]
        public Keys ShortcutKeys
        {
            get { return shortcutKeys; }
            set
            {
                if (shortcutKeys != value)
                {
                    shortcutKeys = value;
                    KeysConverter kc = new KeysConverter();
                    string s = (string)kc.ConvertTo(value, typeof(string));
                    UpdateAllTargets("ShortcutKeyDisplayString", s);
                }
            }
        }

        [DefaultValue(true), UpdatablePropertyAttribute]
        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    UpdateAllTargets("Visible", value);
                }
            }
        }

        protected ActionWorkingState WorkingState { get { return workingState; } set { workingState = value; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        protected internal UIActionsManager ActionList
        {
            get { return actionList; }
            set { if (actionList != value) actionList = value; }
        }

        internal bool InterceptingCheckStateChanged
        {
            get { return interceptingCheckStateChanged; }
            set { interceptingCheckStateChanged = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates this action (raises the <see cref="Update"/> event.
        /// </summary>
        public void DoUpdate() { OnUpdate(EventArgs.Empty); }

        /// <summary>
        /// Runs this action.
        /// </summary>
        public void DoRun()
        {
            if (!enabled) return;

            CancelEventArgs e = new CancelEventArgs();
            OnBeforeRun(e);
            if (e.Cancel) return;

            OnRun(EventArgs.Empty);
            OnAfterRun(EventArgs.Empty);
        }

        #endregion

        #region Protected virtual methods

        /// <summary>
        /// Raises the <see cref="E:BeforeRun"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeRun(CancelEventArgs e)
        {
            if (BeforeRun != null) BeforeRun(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Run"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnRun(EventArgs e)
        {
            if (Run != null) Run(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:AfterRun"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterRun(EventArgs e)
        {
            if (AfterRun != null) AfterRun(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Update"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnUpdate(EventArgs e)
        {
            if (Update != null) Update(this, e);
        }

        /// <summary>
        /// Called when removing the specified extendee from this action's targets list.
        /// </summary>
        /// <param name="extendee">The extended component.</param>
        protected virtual void OnRemovingTarget(Component extendee) { }

        /// <summary>
        /// Called when adding the specified extendee to this action's targets list.
        /// </summary>
        /// <param name="extendee">The extended component.</param>
        protected virtual void OnAddingTarget(Component extendee) { }

        /// <summary>
        /// Adds events handler to the specified extendee.
        /// </summary>
        /// <param name="extendee">The extended component.</param>
        protected virtual void AddHandler(Component extendee)
        {
            EventInfo clickEvent = extendee.GetType().GetEvent("Click");
            if (clickEvent != null)
                clickEvent.AddEventHandler(extendee, clickEventHandler);

            EventInfo checkStateChangedEvent = extendee.GetType().GetEvent("CheckStateChanged");
            if (checkStateChangedEvent != null)
                checkStateChangedEvent.AddEventHandler(extendee, checkStateChangedEventHandler);

            // Cas particulier : bouton de barre d'outils
            ToolBarButton button = extendee as ToolBarButton;
            if (button != null)
                button.Parent.ButtonClick += new ToolBarButtonClickEventHandler(HandleToolbarButtonClick);
        }

        /// <summary>
        /// Removes the previously set events handler from the specified extendee.
        /// </summary>
        /// <param name="extendee">The extended component.</param>
        protected virtual void RemoveHandler(Component extendee)
        {
            EventInfo clickEvent = extendee.GetType().GetEvent("Click");
            if (clickEvent != null)
                clickEvent.RemoveEventHandler(extendee, clickEventHandler);

            EventInfo checkStateChangedEvent = extendee.GetType().GetEvent("CheckStateChanged");
            if (checkStateChangedEvent != null)
                checkStateChangedEvent.RemoveEventHandler(extendee, checkStateChangedEventHandler);

            // Cas particulier : bouton de barre d'outils
            ToolBarButton button = extendee as ToolBarButton;
            if (button != null)
                button.Parent.ButtonClick -= new ToolBarButtonClickEventHandler(HandleToolbarButtonClick);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Removes the specified extendee from this action's targets list.
        /// </summary>
        /// <param name="extendee">The extended component.</param>
        internal void InternalRemoveTarget(Component extendee)
        {
            targets.Remove(extendee);
            RemoveHandler(extendee);
            OnRemovingTarget(extendee);
        }

        /// <summary>
        /// Adds the specified extendee to this action's targets list.
        /// </summary>
        /// <param name="extendee">The extended component.</param>
        internal void InternalAddTarget(Component extendee)
        {
            targets.Add(extendee);
            RefreshState(extendee);
            AddHandler(extendee);
            OnAddingTarget(extendee);
        }

        /// <summary>
        /// Refreshes the <c>Enabled</c> and <c>CheckState</c> properties on all the targets.
        /// </summary>
        internal void RefreshEnabledCheckState()
        {
            UpdateAllTargets("Enabled", this.Enabled);
            UpdateAllTargets("CheckState", this.CheckState);
        }

        /// <summary>
        /// Runs this action.
        /// </summary>
        internal void RunShortcut()
        {
            if (!Enabled) return;
            if (CheckOnClick) Checked = !Checked;
            DoRun();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Updates the specified property with the provided value on all the target components.
        /// </summary>
        /// <param name="propertyName">Name of the property to be updated for all the target components.</param>
        /// <param name="value">The new property value.</param>
        protected void UpdateAllTargets(string propertyName, object value)
        {
            foreach (Component c in targets)
                UpdateProperty(c, propertyName, value);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Updates the specified property with the provided value on the <paramref name="target"/> component.
        /// </summary>
        /// <param name="target">The target component.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The new property value.</param>
        private void UpdateProperty(Component target, string propertyName, object value)
        {
            workingState = ActionWorkingState.Driving;
            try
            {
                if (ActionList != null) ActionList.TypesDescription[target.GetType()].SetValue(
                            propertyName, target, value);
            }
            finally { workingState = ActionWorkingState.Listening; }
        }

        /// <summary>
        /// Refreshes the psecified components' state.
        /// </summary>
        /// <param name="target">The target component.</param>
        private void RefreshState(Component target)
        {
            PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(this, new Attribute[] { new UpdatablePropertyAttribute() });

            foreach (PropertyDescriptor property in properties)
                UpdateProperty(target, property.Name, property.GetValue(this));
        }

        /// <summary>
        /// Handles the specified <c>Click</c> event.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleClick(object sender, EventArgs e)
        {
            if (workingState == ActionWorkingState.Listening)
            {
                Component target = sender as Component;
                Debug.Assert(target != null, "Target isn't a component with HandleClick");
                Debug.Assert(targets.Contains(target), "Target non esiste su collection targets su handleClick");

                DoRun();
            }
        }

        /// <summary>
        /// Handles the specified <c>ToolBarButtonClick</c> event.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ToolBarButtonClickEventArgs"/> instance containing the event data.</param>
        private void HandleToolbarButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (targets.Contains(e.Button)) 
                HandleClick(e.Button, e); // called if sender is ToolBarButton
        }

        /// <summary>
        /// Handles the specified <c>CheckStateChanged</c> event.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleCheckStateChanged(object sender, EventArgs e)
        {
            if (workingState == ActionWorkingState.Listening)
            {
                Component target = sender as Component;
                CheckState = (CheckState)ActionList.
                    TypesDescription[sender.GetType()].GetValue("CheckState", sender);
            }
        }

        #endregion

        #region Events handling

        ///// <summary>
        ///// Handles the ButtonClick event of the toolbar control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.Windows.Forms.ToolBarButtonClickEventArgs"/> instance containing the event data.</param>
        //private void toolbar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        //{
        //    if (targets.Contains(e.Button)) HandleClick(e.Button, e); // called if sender is ToolBarButton
        //}

        ///// <summary>
        ///// Handles the Click event of the target control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //private void target_Click(object sender, EventArgs e)
        //{
        //    HandleClick(sender, e); // called if sender is Control
        //}

        ///// <summary>
        ///// Handles the CheckStateChanged event of the target control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //private void target_CheckStateChanged(object sender, EventArgs e)
        //{
        //    HandleCheckStateChanged(sender, e);
        //}

        #endregion
    }   
}
