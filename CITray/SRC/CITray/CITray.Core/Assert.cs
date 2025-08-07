using System;
using System.Diagnostics;

namespace CITray
{
    /// <summary>
    /// For debugging purpose
    /// </summary>
    internal static class Assert
    {
        public static void IsTrue(bool condition) { IsTrue(condition, null); }
        public static void IsTrue(bool condition, string message)
        {
            if (!condition) AssertionFailed(message);
        }

        public static void IsFalse(bool condition) { IsFalse(condition, null); }
        public static void IsFalse(bool condition, string message) { IsTrue(!condition, message); }

        public static void IsNull(object toTest) { IsNull(toTest, null); }
        public static void IsNull(object toTest, string message)
        {
            IsTrue(object.ReferenceEquals(toTest, null), message);
        }

        public static void IsNotNull(object toTest) { IsNotNull(toTest, null); }
        public static void IsNotNull(object toTest, string message)
        {
            IsFalse(object.ReferenceEquals(toTest, null), message);
        }

        private static void AssertionFailed(string message)
        {
            if (Debugger.IsAttached) Debugger.Break();
            else Debug.Fail(message ?? "Assertion failed in CITray");
        }
    }
}
