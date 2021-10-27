using ActivityReporting.App.Api.Model;

namespace ActivityReporting.App.Api.Interfaces
{
    public interface IDatabase
    {
        public ulong Sum(string key);
        public void Log(IActivityLog activity);
    }
}
