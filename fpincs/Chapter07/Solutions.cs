using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using static LaYumba.Functional.F;

namespace fpincs.Chapter07
{
    internal static class Solutions
    {
        // Exercise 01
        public static Func<int, int, int> Remainder
            = (x, y) => x - x / y * y;

        private static Func<T1, R> ApplyR<T1, T2, R>(this Func<T1, T2, R> func, T2 t2)
            => t1 => func(t1, t2);

        private static Func<int, int> RemainderOfDividingByFive
            = Remainder.ApplyR(5);

        private static Func<T1, T2, R> ApplyR<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T3 t3)
            => (t1, t2) => func(t1, t2, t3);

        // Exercise 02
        private enum NumberType { Mobile, Home, Office }

        private class CountryCode
        {
            private string Value { get; }

            public CountryCode(string value)
            {
                Value = value;
            }

            public static implicit operator string(CountryCode c) => c.Value;

            public static implicit operator CountryCode(string s) => new(s);

            public override string ToString() => Value;
        }

        private class PhoneNumber
        {
            public NumberType Type { get; }
            public CountryCode Country { get; }
            public string Number { get; }

            public PhoneNumber(NumberType type, CountryCode country, string number)
            {
                Type = type;
                Country = country;
                Number = number;
            }
        }

        private static readonly Func<CountryCode, NumberType, string, PhoneNumber> CreatePhoneNumber
            = (country, type, number) => new PhoneNumber(type, country, number);

        private static readonly Func<NumberType, string, PhoneNumber> CreateUKNumber
            = CreatePhoneNumber.Apply((CountryCode)"UK");

        private static readonly Func<string, PhoneNumber> CreateUKMobileNumber
            = CreateUKNumber.Apply(NumberType.Mobile);

        // Exercise 03
        private enum Level { Debug, Info, Error }

        private delegate void Log(Level level, string message);

        private static readonly Log ConsoleLogger = (level, message)
            => Console.WriteLine($"[{level}]: {message}");

        private static void Debug(this Log log, string message)
            => log(Level.Debug, message);

        private static void Info(this Log log, string message)
            => log(Level.Info, message);

        private static void Error(this Log log, string message)
            => log(Level.Error, message);

        public static void Test()
            => ConsumeLog(ConsoleLogger);

        private static void ConsumeLog(Log log)
            => log.Info("info");

        // Exercise 05
        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> f)
            => ts.Aggregate(List<R>(), (rs, t) => rs.Append(f(t)));

        private static IEnumerable<R> Bind<T, R>(this IEnumerable<T> ts, Func<T, IEnumerable<R>> f)
            => ts.Aggregate(List<R>(), (rs, t) => rs.Concat(f(t)));

        private static IEnumerable<T> Where<T>(this IEnumerable<T> @this, Func<T, bool> predicate)
            => @this.Aggregate(List<T>(), (ts, t) => predicate(t) ? ts.Append(t) : ts);
    }
}