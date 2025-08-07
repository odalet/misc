using System;

namespace TestLib
{
    public static class Operations
    {
        public static double Add(double x, double y) => x + y;
        public static double Subtract(double x, double y) => x - y;
        public static double Multiply(double x, double y) => x * y;
        public static double Divide(double x, double y) => x / y;
        public static double Pow(this double x, double y) => Math.Pow(x, y);
        public static double Sqrt(double x) => Math.Sqrt(x);
        public static double Cos(double x) => Math.Cos(x);
    }
}
