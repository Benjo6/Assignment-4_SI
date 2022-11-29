using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class ResetPasswordRequestObject
{
    [Required]
    [StringLength(500)]
    [EmailAddress]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
        ErrorMessage = "No valid email address provided")]
    public string Email { get; set; }
}