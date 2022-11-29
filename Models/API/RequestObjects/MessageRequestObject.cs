using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.RequestObjects;

public class MessageRequestObject
{
    [Required] public string Message { get; set; }
}