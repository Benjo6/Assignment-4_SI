using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.ResponseObjects;

public class ReservationResponseObject
{
    [Display(Name = "id")]
    public long Id { get; set; }
    [Display(Name = "activityid")]
    public long ActivityId { get; set; }
    [Display(Name = "visitorid")]
    public long VisitorId{ get; set; }
    [Display(Name = "payedfee")]
    public bool PayedFee{ get; set; }
    [Display(Name = "hasattended")]
    public bool HasAttended{ get; set; }
    [Display(Name = "withinvoice")]
    public bool WithInvoice { get; set; }}