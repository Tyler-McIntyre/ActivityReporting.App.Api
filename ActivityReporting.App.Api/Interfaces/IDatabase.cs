using ActivityReporting.App.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityReporting.App.Api.Interfaces
{
    public interface IDatabase
    {
        public ulong Sum(string key);
        public void Log(ActivityLogReq activity);
    }
    class InMemDatabase : IDatabase
    {
        private static List<ActivityLogReq> activityList = new();
        private const double _delay = 120; //seconds

        public InMemDatabase()
        {
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
                              where x.Key == key &
                              x.CreatedDateTime > DateTime.Now.AddHours(-12)
                              select x.Value).Sum());
            }

        void IDatabase.Log(ActivityLogReq activity)
        {
            activityList.Add(activity);
        }

        static void Prune()
        {
            activityList = (from x in activityList
                            where x.CreatedDateTime > DateTime.Now.AddHours(-12)
                            select x).ToList();
        }
        
    }
}
