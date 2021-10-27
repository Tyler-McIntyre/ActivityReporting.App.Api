namespace ActivityReporting.App.Api.Model
{
    public class ActivityResponse : IActivityResp
    {
        public ulong Value { get; init; }

        public ActivityResponse(ulong value)
        {
            Value = value;
        }
    }
}
