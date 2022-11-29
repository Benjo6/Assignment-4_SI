using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class ReservationCreateObject
{
    [Required]
    [Display(Name = "activityid")]
    public long ActivityId { get; set; }
    [Required]
    [Display(Name = "visitorid")]
    public long VisitorId { get; set; }
    [Required]
    [Display(Name = "withinvoice")]
    public bool WithInvoice{ get; set; }
}