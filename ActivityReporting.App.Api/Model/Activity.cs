using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActivityReporting.App.Api.Model
{
    public class ActivityLogReq
    {
        [DefaultValue("Generic Activity")]
        [StringLength(maximumLength:50, ErrorMessage = "Please enter a key with 50 characters or less.", MinimumLength = 1)]
        public string Key { get; private set; }

        [Required]
        [DefaultValue(1)]
        [Range(1,100, ErrorMessage = "Please enter a number between 1 & 100")]
        public decimal Value { get; set; }
        public DateTime CreatedDateTime { get; private set; }

        public ActivityLogReq()
        {
            CreatedDateTime = DateTime.Now;
        }

        public void SetKey(string key)
        {
            Key = key.ToUpper();
        }
    }

    public class ActivityResp
    {
        public ulong Value { get; init; }

        public ActivityResp(ulong value)
        {
            Value = value;
        }
    }
}
