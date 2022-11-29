using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class ActivationRequestObject
{
    [Required]
    [StringLength(500)]
    [Display(Name = "pass")]
    public string Pass { get; set; }
    [Required]
    [StringLength(500)]
    [EmailAddress]
    [Display(Name = "email")]
    public string Email { get; set; }
}