using System.ComponentModel.DataAnnotations;
using Assignment_4_SI.Validation;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class ActivityUpdateObject
{
    //[Display(Name = "isActive")]
    //public bool? IsActive { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 2)]
    [Display(Name = "description")]
    public string Description { get; set; }
    [Required]
    [Display(Name = "starttime")]
    [DateValidation]

    public string StartTime { get; set; }
    [Required]
    [Display(Name = "endtime")]
    [DateValidation]

    public string EndTime { get; set; }
    [Display(Name = "speakerId")]
    public long? SpeakerId { get; set; }
    [Required]
    [Display(Name = "price")]
    public decimal Price { get; set; }
    [Required]
    [Display(Name = "remainingcapacity")]
    public int RemainingCapacity { get; set; }
}