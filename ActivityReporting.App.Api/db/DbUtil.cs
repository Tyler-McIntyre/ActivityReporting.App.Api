using ActivityReporting.App.Api.Interfaces;
using ActivityReporting.App.Api.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityReporting.App.Api.DB
{
    public class DbUtil
    {
        private readonly ILogger _log = Log.Logger;
        private const double _delay = 30; //seconds

        public DbUtil()
        {
            _log.Information("Init InMemDatabase");

            Task.Run(async () =>
            {
                while (true)
                {
                    Prune(InMemDatabase.ActivityDbList);
                    await Task.Delay(TimeSpan.FromSeconds(_delay));
                }
            });
        }

        void Prune(List<IActivityLog> activityList)
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
