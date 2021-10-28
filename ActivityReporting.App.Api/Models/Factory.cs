﻿namespace ActivityReporting.App.Api.Model
{
    public class Factory
    {
        public static IActivityLog CreateNewActivityLog() {

            return new ActivityLog();
        }

        public static IActivityResponse CreateNewActivityResponse(ulong value)
        {
            return new ActivityResponse(value);
        }
    }
}