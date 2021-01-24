using System;

namespace fpincs.Chapter05
{
    internal static class Solutions
    {
        private static Func<T1, R> Compose<T1, T2, R>(this Func<T2, R> g, Func<T1, T2> f)
            => x => g(f(x));
    }
}