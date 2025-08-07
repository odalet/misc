using System;

namespace TPropertyGrid
{
	public abstract class TAttribute : Attribute
	{
		protected string key  = "";
		protected string defval = "";

		public TAttribute(string translationKey) : 
			this(translationKey, "") {}

		public TAttribute(string translationKey, string defaultValue)
		{
			key = translationKey;
			defval = defaultValue;
		}

		public string Key { get { return key; } }
		public string DefaultValue { get { return defval; } }
		public virtual string Text { get { return T.Get(key, defval); } }
	}
	
	[AttributeUsage(AttributeTargets.Property)]
	public class TNameAttribute : TAttribute 
	{
		public TNameAttribute(string translationKey) : base(translationKey) {}
		public TNameAttribute(string translationKey, string defaultValue) : base(translationKey, defaultValue) {}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class TCategoryAttribute : TAttribute 
	{				
		public TCategoryAttribute(string translationKey) : base(translationKey) {}		
		public TCategoryAttribute(string translationKey, string defaultValue) : base(translationKey, defaultValue) {}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class TDescriptionAttribute : TAttribute 
	{
		public TDescriptionAttribute(string translationKey) : base(translationKey) {}
		public TDescriptionAttribute(string translationKey, string defaultValue) : base(translationKey, defaultValue) {}
	}
}
