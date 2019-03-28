using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginRegistration.Models
{
    public class Register
    {
       [Key]
       public int UserId {get;set;}

       [Required]
       [MinLength(2)]
       [Display(Name="First Name: ")]
       public string FirstName {get;set;}

       [Required]
       [MinLength(2)]
       [Display(Name="Last Name: ")]
       public string LastName {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name="Email: ")]

       public string Email {get;set;}

       [Required]
       [DataType(DataType.Password)]
       [MinLength(5, ErrorMessage = "Password must be at least 5 characters or longer")]
       public string Password {get;set;}

       [NotMapped]
       [Compare("Password")]
       [DataType(DataType.Password)]
       public string Confirm{get;set;}
       public DateTime CreatedAt {get;set;}
       public DateTime UpdatedAt {get;set;}
    }

    public class Login
    {
        public int UserId {get;set;}

        [Required]
        [EmailAddress]
        public string Email{get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt{get;set;}
    }
}