using System;
using System.ComponentModel;

using TPropertyGrid;


namespace TestApp
{
    /// <summary>
    /// Summary description for TestObject.
    /// </summary>

    [TypeConverter(typeof(TExpandableObjectConverter))]
    public class TestObject
    {
        private string name = string.Empty;
        private int age = 0;
        private float height = 0f;

        private TestObject mySister1 = null;
        private TestObject mySister2 = null;
        private TestObject[] mySisters = null;

        public TestObject()
        {
            name = "Olivier DALET";
            age = 28;
            height = 1.8f;
            mySister1 = new TestObject("Claire DALET", 24, 1.68f);
            mySister2 = new TestObject("Laure DALET", 22, 1.59f);
            mySisters = new TestObject[] { mySister1, mySister2 };
        }

        public TestObject(string n, int a, float h)
        {
            name = n;
            age = a;
            height = h;
        }

        [Description("This is the object's name")]
        [Category("Strings")]
        [TName("Name", "Name")]
        [TDescription("NameDesc", "The object's name")]
        [TCategory("Strings", "Strings")]
        public string Name { get { return name; } set { name = value; } }

        [Description("This is the object's age")]
        [Category("Numbers")]
        [TName("Age", "Age")]
        [TDescription("AgeDesc", "How old am I?")]
        [TCategory("Numbers", "Numbers")]
        public int Age { get { return age; } set { age = value; } }

        [Description("This is the object's height")]
        [Category("Numbers")]
        [TName("Height", "Height")]
        [TDescription("HeightDesc")]
        [TCategory("Numbers", "Numbers")]
        public float Height { get { return height; } set { height = value; } }

        [Description("This is my 1st sister")]
        [Category("Family")]
        [TName("Sister1", "Sister 1")]
        [TDescription("Sister1Desc", "My first sister")]
        [TCategory("Family", "Family")]
        public TestObject MySister1 { get { return mySister1; } }

        [Description("This is my 2nd sister")]
        [Category("Family")]
        [TName("Sister2", "Sister 2")]
        [TDescription("Sister2Desc", "My second sister")]
        [TCategory("Family", "Family")]
        public TestObject MySister2 { get { return mySister2; } }

        [Description("These are my sisters")]
        [Category("Family")]
        [TName("Sisters", "Sisters")]
        [TDescription("SistersDesc", "My two sisters")]
        [TCategory("Family", "Family")]
        public TestObject[] MySisters { get { return mySisters; } }
    }
}
