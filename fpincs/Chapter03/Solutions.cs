using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using static LaYumba.Functional.F;

namespace fpincs.Chapter03
{
    internal static class Solutions
    {
        // Exercise 01
        public static Option<TEnum> Parse<TEnum>(this string value) where TEnum : struct, System.Enum
            => System.Enum.TryParse(value, out TEnum result) ? Some(result) : None;

        // Exercise 02
        public static Option<T> Lookup<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (var item in list)
                if (predicate(item))
                    return Some(item);

            return None;
        }

        // Exercise 03
        public class Email
        {
            private static readonly Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            private string Value { get; }

            private Email(string value) => Value = value;

            public static Option<Email> Create(string s)
                => regex.IsMatch(s)
                    ? Some(new Email(s))
                    : None;

            public static implicit operator string(Email e)
                => e.Value;
        }

        // Exercise 05
        public class AppConfig
        {
            private readonly NameValueCollection source;

            public AppConfig(NameValueCollection source)
            {
                this.source = source;
            }

            public Option<T> Get<T>(string key)
                => source[key] == null
                    ? None
                    : Some((T)Convert.ChangeType(source[key], typeof(T)));

            public T Get<T>(string key, T defaultValue)
                => Get<T>(key).Match(
                    () => defaultValue,
                    (value) => value);
        }
    }
}