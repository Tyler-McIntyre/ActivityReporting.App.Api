using ActivityReporting.App.Api.DB;
using ActivityReporting.App.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityReporting.App.Api.Interfaces
{
    public class InMemDatabase
    {
        public static List<IActivityLog> ActivityDbList = new();

        public InMemDatabase()
        {
            _ = new DbUtil();
        }

        public static ulong Sum(string key)
        {
            return (ulong)Math.Round((from x in ActivityDbList
                                      where x.Key.ToUpper() == key.ToUpper() &
                              x.CreatedDateTime > DateTime.Now.AddMinutes(-12)
                                      select x.Value).Sum());
        }

        public static void Log(IActivityLog activity)
        {
            ActivityDbList.Add(activity);
        }
    }
}
