using System;
using System.Collections.Generic;
using System.Linq;

namespace fpincs.Chapter01
{
    internal static class Solutions
    {
        // Exercise 02
        public static Func<T, bool> Negate<T>(this Func<T, bool> predicate)
            => t => !predicate(t);

        // Exercise 03
        public static List<int> QuickSort(this List<int> list)
        {
            if (!list.Any())
                return new List<int>();

            var x = list.First();
            var xs = list.Skip(1).ToList();

            var left = xs.Where(i => i <= x).ToList();
            var right = xs.Where(i => i > x).ToList();

            return left.QuickSort().Append(x).Concat(right.QuickSort()).ToList();
        }

        // Exercise 04
        public static List<T> QuickSort<T>(this List<T> list, Comparison<T> compare)
        {
            if (!list.Any())
                return new List<T>();

            var x = list.First();
            var xs = list.Skip(1).ToList();

            var left = xs.Where(i => compare(i, x) < 1).ToList();
            var right = xs.Where(i => compare(i, x) >= 1).ToList();

            return left.QuickSort(compare).Append(x).Concat(right.QuickSort(compare)).ToList();
        }

        // Exercise 05
        public static R Using<TDisp, R>(Func<TDisp> createDisposable, Func<TDisp, R> func)
            where TDisp : IDisposable
        {
            using var disp = createDisposable();
            return func(disp);
        }
    }
}