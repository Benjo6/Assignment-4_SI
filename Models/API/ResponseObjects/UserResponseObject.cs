using System.ComponentModel.DataAnnotations;

namespace Assignment_4_SI.Models.API.ResponseObjects;

public class UserResponseObject
{
    //[Display(Name = "isactive")]
    //public bool IsActive { get; set; }
    [Display(Name="id")]
    public long Id { get; set; }
    [Display(Name = "firstname")]
    public string Firstname { get; set; }
    [Display(Name = "lastname")]
    public string Lastname { get; set; }
    [Display(Name = "email")]
    public string Email { get; set; }
    [Display(Name = "street")]
    public string Street { get; set; }
    [Display(Name = "number")]
    public int Number { get; set; }
    [Display(Name = "bus")]
    public string Bus { get; set; }
    [Display(Name = "zipcode")]
    public int ZipCode { get; set; }
    [Display(Name = "city")]
    public string City { get; set; }
    [Display(Name = "roles")]
    public string[] Roles { get; set; }
    //[Display(Name = "activities")]
    //public HashSet<Activity> Activities { get; set; }
}