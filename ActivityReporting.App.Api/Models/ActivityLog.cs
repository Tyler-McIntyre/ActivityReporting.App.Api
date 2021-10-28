using Serilog;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActivityReporting.App.Api.Model
{
    public class ActivityLog : IActivityLog
    {
        private readonly ILogger _log = Log.Logger;

        [DefaultValue("Generic Activity")]
        [StringLength(maximumLength: 50, ErrorMessage = "Please enter a key with 50 characters or less.", MinimumLength = 1)]
        public string Key { get; private set; }

        [Required]
        [DefaultValue(1)]
        [Range(1, 100, ErrorMessage = "Please enter a number between 1 & 100")]
        public decimal Value { get; set; }
        public DateTime CreatedDateTime { get; private set; }

        public ActivityLog()
        { 
            CreatedDateTime = DateTime.Now;
        }

        public void SetKey(string key)
        {
            _log.Information($"Set Request Key: {key}");
            Key = key;
        }
    }
}
