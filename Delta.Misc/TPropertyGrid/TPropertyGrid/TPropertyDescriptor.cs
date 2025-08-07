using System;
using System.ComponentModel;

namespace TPropertyGrid
{
	internal class TPropertyDescriptor : PropertyDescriptor
	{
		private PropertyDescriptor property; 
		private string name = string.Empty;
		private string description = string.Empty;
		private string category = string.Empty;
		private bool cachedata = false;
		
		public TPropertyDescriptor(PropertyDescriptor baseProperty):
			base(baseProperty) { property = baseProperty; }
		
		#region Overrides
		public override bool CanResetValue(object component) { return property.CanResetValue(component); }
		public override Type ComponentType { get { return property.ComponentType; } }
		public override object GetValue(object component) { return property.GetValue(component); }
		public override bool IsReadOnly { get { return property.IsReadOnly; } }
		public override Type PropertyType { get { return property.PropertyType; } }
		public override void ResetValue(object component) { property.ResetValue(component); }
		public override void SetValue(object component, object value) { property.SetValue(component, value); }
		public override bool ShouldSerializeValue(object component) { return property.ShouldSerializeValue(component); }

		public override string DisplayName
		{
			get
			{
				if ((name == string.Empty) || (!cachedata)) BuildName();
				return name;				
			}
		}

		public override string Description
		{
			get
			{
				if ((description == string.Empty) || (!cachedata)) BuildDescription();
				return description;				
			}
		}

		public override string Category
		{
			get
			{
				if ((category == string.Empty) || (!cachedata)) BuildCategory();
				return category;				
			}
		}

		#endregion

		private void BuildName()
		{
			name = property.DisplayName;
			foreach(Attribute attribute in property.Attributes)
			{
				if (attribute.GetType().Equals(typeof(TNameAttribute)))
					name = BuildString(attribute as TAttribute, name);
			}
		}

		private void BuildDescription()
		{
			description = property.Description;
			foreach(Attribute attribute in property.Attributes)
			{
				if (attribute.GetType().Equals(typeof(TDescriptionAttribute)))
					description = BuildString(attribute as TAttribute, description);
			}
		}

		private void BuildCategory()
		{
			category = property.Category;
			foreach(Attribute attribute in property.Attributes)
			{
				if (attribute.GetType().Equals(typeof(TCategoryAttribute)))
					category = BuildString(attribute as TAttribute, category);
			}
		}

		private string BuildString(TAttribute attribute, string original)
		{
			if ((attribute.Key == string.Empty) && (attribute.DefaultValue == string.Empty)) 
				return original;
			else
			{
				string defval = original;
				if (attribute.DefaultValue != string.Empty) defval = attribute.DefaultValue;

				string trans = T.Get(attribute.Key);
				if ((trans == string.Empty) || (trans == attribute.Key)) return defval;
				else return trans;
			}
		}
	}
}

