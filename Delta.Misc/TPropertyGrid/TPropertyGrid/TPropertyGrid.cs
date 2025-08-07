using System;
using System.Windows.Forms;

namespace TPropertyGrid
{
	public class TPropertyGrid : PropertyGrid
	{
		private object selectedObject = null;
		private object[] selectedObjects = null;

		public TPropertyGrid() : base() {}

		public new object SelectedObject
		{
			get { return selectedObject; }
			set { SelectObject(value); }
		}

		public new object[] SelectedObjects
		{
			get { return selectedObjects; }
			set { SelectObjects(value); }
		}

		private void SelectObject(object o)
		{
			if (o == null)
				selectedObject = base.SelectedObject = null;
			else
			{
				selectedObject = o;
				base.SelectedObject = new TObjectWrapper(o);
			}
		}

		private void SelectObjects(object[] objects)
		{
			if (objects == null)
				selectedObjects = base.SelectedObjects = null;
			else
			{
				selectedObjects = objects;
				object[] newObjects = new object[objects.Length];

				for (int i=0; i<objects.Length; i++)
					newObjects[i] = new TObjectWrapper(objects[i]);

				base.SelectedObjects = newObjects;
			}
		}
	}
		
}

