using System;
using System.ComponentModel;

namespace TPropertyGrid
{
	public class TExpandableObjectConverter : ExpandableObjectConverter
	{
		public TExpandableObjectConverter() {}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is TObjectWrapper) return base.GetProperties (context, value, attributes);
			else return base.GetProperties (context, new TObjectWrapper(value), attributes);
		}
	}
}
