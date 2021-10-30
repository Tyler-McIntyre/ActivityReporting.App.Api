using ActivityReporting.App.Api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ActivityReporting.App.Api.Models
{
    public class ActivityDto : IActivityDto
    {
        [Required]
        public decimal Value { get; set; }

        public ActivityDto()
        { 

        }
    }
}
