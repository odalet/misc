using System;
using System.ComponentModel;

namespace TPropertyGrid
{
	internal class TObjectWrapper : ICustomTypeDescriptor
	{
		private object o = null;
		private PropertyDescriptorCollection properties = null;

		public TObjectWrapper(object obj) 
		{ 
			if (obj == null) throw new ArgumentNullException("obj");
			o = obj; 
		}

		private PropertyDescriptorCollection GetProps(Attribute[] attributes)
		{
			// Only do once
			if (properties == null) 
			{
				PropertyDescriptorCollection baseProps = null;
				if (attributes == null) baseProps = TypeDescriptor.GetProperties(o, true);
				else baseProps = TypeDescriptor.GetProperties(o, attributes, true);
				properties = new PropertyDescriptorCollection(null);

				// For each property use a property descriptor of our own 
				// that is able to be globalized
				foreach(PropertyDescriptor property in baseProps)
					properties.Add(new TPropertyDescriptor(property));
			}
			return properties;
		}

		#region ICustomTypeDescriptor Members

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProps(attributes);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return GetProps(null);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(o, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(o, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(o, true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(o, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return o;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(o, true);
		}		

		public object GetEditor(Type editorBaseType)
		{
			if (o == null) return null;
			return TypeDescriptor.GetEditor(o, editorBaseType, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(o, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(o, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(o, true);
		}

		#endregion
	}

}
