using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class ReservationUpdateObject
{
    [Required]
    [Display(Name = "payedfee")]
    public bool PayedFee { get; set; }
    [Required]
    [Display(Name = "hasattended")]
    public bool HasAttended { get; set; }
    [Required]
    [Display(Name = "withinvoice")]
    public bool WithInvoice { get; set; }
}