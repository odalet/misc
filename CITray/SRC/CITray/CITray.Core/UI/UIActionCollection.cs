/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;

namespace CITray.Core.UI
{
    /// <summary>
    /// A collection of <see cref="UIAction"/> objects
    /// </summary>
    [Editor(typeof(UIActionCollectionEditor), typeof(UITypeEditor))]
    public class UIActionCollection: Collection<UIAction>
    {
        /// <summary>
        /// The parent Actions manager.
        /// </summary>
        private UIActionsManager parent = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIActionCollection"/> class.
        /// </summary>
        /// <param name="parentList">The parent list.</param>
        public UIActionCollection(UIActionsManager parentList) { parent = parentList; }

        /// <summary>
        /// Gets the parent Actions manager.
        /// </summary>
        /// <value>The parent.</value>
        public UIActionsManager Parent { get { return parent; } }

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (UIAction action in this) action.ActionList = null;
            base.ClearItems();
        }

        /// <summary>
        /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is less than zero.
        /// -or-
        /// <paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.
        /// </exception>
        protected override void InsertItem(int index, UIAction item)
        {
            // This check is needed because BaseDockingForm may add the same item multiple times...
            if (base.Contains(item)) return;

            base.InsertItem(index, item);
            item.ActionList = Parent;
        }

        /// <summary>
        /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is less than zero.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.
        /// </exception>
        protected override void RemoveItem(int index)
        {
            this[index].ActionList = null;
            base.RemoveItem(index);
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is less than zero.
        /// -or-
        /// <paramref name="index"/> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count"/>.
        /// </exception>
        protected override void SetItem(int index, UIAction item)
        {
            if (base.Count > index) this[index].ActionList = null;
            base.SetItem(index, item);

            item.ActionList = Parent;
        }
    }
}
