using LaYumba.Functional;
using System;
using static LaYumba.Functional.F;

namespace fpincs.Chapter06
{
    internal static class Solutions
    {
        // Exercise 01
        private static Option<R> ToOption<L, R>(this Either<L, R> @this)
            => @this.Match(_ => None, Some);

        private static Either<L, R> ToEither<L, R>(this Option<R> @this, Func<L> left)
            => @this.Match<Either<L, R>>(() => left(), r => r);

        // Exercise 02
        public static Option<RR> Bind<L, R, RR>(this Either<L, R> @this, Func<R, Option<RR>> func)
            => @this.Match(_ => None, func);

        public static Option<RR> Bind<L, R, RR>(this Option<R> @this, Func<R, Either<L, RR>> func)
            => @this.Match(() => None, v => func(v).ToOption());

        // Exercise 03
        public static Exceptional<T> TryRun<T>(Func<T> f)
        {
            try { return f(); }
            catch (Exception e) { return e; }
        }

        // Exercise 04
        public static Either<L, R> Safely<L, R>(Func<R> f, Func<Exception, L> left)
        {
            try { return f(); }
            catch (Exception e) { return left(e); }
        }
    }
}