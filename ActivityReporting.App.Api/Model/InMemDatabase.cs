using ActivityReporting.App.Api.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityReporting.App.Api.Interfaces
{
    public class InMemDatabase : IDatabase
    {
        private static List<IActivityLog> activityList = new();
        private readonly ILogger _log = Log.Logger;
        private const double _delay = 30; //seconds
        
        public InMemDatabase()
        {
            _log.Information("Init InMemDatabase");

            Task.Run(async () =>
            {
                while (true)
                {
                    Prune();
                    await Task.Delay(TimeSpan.FromSeconds(_delay));
                }
            });
        }

        ulong IDatabase.Sum(string key){
            return (ulong)Math.Round((from x in activityList
                              where x.Key.ToUpper() == key.ToUpper() &
                              x.CreatedDateTime > DateTime.Now.AddMinutes(-12)
                              select x.Value).Sum());
            }

        void IDatabase.Log(IActivityLog activity)
        {
            activityList.Add(activity);
        }

        void Prune()
        {
            if (activityList.Count > 0)
            {
                List<IActivityLog> pruneList = (from x in activityList
                                                where x.CreatedDateTime < DateTime.Now.AddMinutes(-1)
                                                select x).ToList();

                if (pruneList.Count > 0)
                {
                    foreach (IActivityLog activity in pruneList)
                    {
                        _log.Information($"Removing Activity Log {activity.Key}, " +
                            $"{activity.Value}, " +
                            $"{activity.CreatedDateTime}");
                    }

                    activityList = activityList
                        .Except(pruneList)
                        .ToList();
                }
            }
        }
    }
}
