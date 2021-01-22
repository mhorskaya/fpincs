using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using static LaYumba.Functional.F;

namespace fpincs.Chapter04
{
    internal static class Solutions
    {
        // Exercise 01
        private static ISet<R> Map<T, R>(this ISet<T> ts, Func<T, R> f)
        {
            var rs = new HashSet<R>();
            foreach (var t in ts)
                rs.Add(f(t));
            return rs;
        }

        private static IDictionary<K, R> Map<K, T, R>(this IDictionary<K, T> dict, Func<T, R> f)
        {
            var rs = new Dictionary<K, R>();
            foreach (var (key, value) in dict)
                rs[key] = f(value);
            return rs;
        }

        // Exercise 02
        public static Option<R> Map<T, R>(this Option<T> opt, Func<T, R> f)
        {
            return opt.Bind(t => Some(f(t)));
        }

        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> f)
        {
            return ts.Bind(t => List(f(t)));
        }

        // Exercise 03
        private static Option<WorkPermit> GetWorkPermit(Dictionary<string, Employee> people, string employeeId)
        {
            return people.Lookup(employeeId).Bind(e => e.WorkPermit);
        }

        private static Option<WorkPermit> GetWorkPermitEnriched(Dictionary<string, Employee> people, string employeeId)
        {
            Func<WorkPermit, bool> hasExpired = permit => permit.Expiry < DateTime.Now.Date;
            return people.Lookup(employeeId).Bind(e => e.WorkPermit).Where(hasExpired.Negate());
        }

        // Exercise 04
        private static double AverageYearsWorkedAtTheCompany(List<Employee> employees)
        {
            static double YearsBetween(DateTime start, DateTime end) => (end - start).Days / 365d;
            return employees.Bind(e => e.LeftOn.Map(leftOn => YearsBetween(e.JoinedOn, leftOn))).Average();
        }
    }

    public struct WorkPermit
    {
        public string Number { get; set; }
        public DateTime Expiry { get; set; }
    }

    public class Employee
    {
        public string Id { get; set; }
        public Option<WorkPermit> WorkPermit { get; set; }

        public DateTime JoinedOn { get; }
        public Option<DateTime> LeftOn { get; }
    }
}