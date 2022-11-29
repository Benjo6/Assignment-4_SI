using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class LoginRequestObject
{
    [Required]
    [Display(Name = "email")]
    public string Email { get; set; }
    [Required]
    [Display(Name = "password")]
    public string Password { get; set; }
}