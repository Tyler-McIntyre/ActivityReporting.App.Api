using Serilog;

namespace ActivityReporting.App.Api.Model
{
    public class ActivityResponse : IActivityResponse
    {
        private readonly ILogger _log = Log.Logger;

        public ulong Value { get; init; }

        public ActivityResponse(ulong value)
        {
            Value = value;

            _log.Information($"Init Response Value: {value}");
        }
    }
}
