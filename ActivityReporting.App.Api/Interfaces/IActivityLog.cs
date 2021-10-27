using System;

namespace ActivityReporting.App.Api.Model
{
    public interface IActivityLog
    {
        DateTime CreatedDateTime { get; }
        string Key { get; }
        decimal Value { get; }

        void SetKey(string key);
    }
}