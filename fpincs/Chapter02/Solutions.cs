using NUnit.Framework;
using System;

namespace fpincs.Chapter02
{
    using static Console;
    using static Math;

    public enum BmiRange { Underweight, Healthy, Overweight }

    internal static class Bmi
    {
        public static void Run()
            => Run(Read, Write);

        internal static void Run(Func<string, double> read, Action<BmiRange> write)
        {
            var weight = read("weight");
            var height = read("height");
            var bmiRange = CalculateBmi(weight, height).ToBmiRange();
            write(bmiRange);
        }

        internal static double CalculateBmi(double weight, double height)
            => Round(weight / Pow(height, 2), 2);

        internal static BmiRange ToBmiRange(this double bmi)
            => bmi < 18.5 ? BmiRange.Underweight : 25 <= bmi ? BmiRange.Overweight : BmiRange.Healthy;

        private static double Read(string field)
        {
            WriteLine($"Please enter your {field}");
            return double.Parse(ReadLine());
        }

        private static void Write(BmiRange bmiRange)
            => WriteLine($"Based on your BMI, you are {bmiRange}");
    }

    public class BmiTests
    {
        [TestCase(77, 1.80, ExpectedResult = 23.77)]
        [TestCase(77, 1.60, ExpectedResult = 30.08)]
        public double CalculateBmi(double weight, double height)
            => Bmi.CalculateBmi(weight, height);

        [TestCase(23.77, ExpectedResult = BmiRange.Healthy)]
        [TestCase(30.08, ExpectedResult = BmiRange.Overweight)]
        public BmiRange ToBmiRange(double bmi)
            => bmi.ToBmiRange();

        [TestCase(77, 1.80, ExpectedResult = BmiRange.Healthy)]
        [TestCase(77, 1.60, ExpectedResult = BmiRange.Overweight)]
        public BmiRange ReadBmi(double weight, double height)
        {
            var result = default(BmiRange);
            Bmi.Run(Read, Write);
            return result;

            double Read(string s) => s == "weight" ? weight : height;
            void Write(BmiRange r) => result = r;
        }
    }
}