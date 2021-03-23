using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class Registeration
    {
        [Key]
        public int UserId { get; set; }
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    public string PasswordSalt { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }

        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public long ContactNumber { get; set; }
        public string UserType { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public bool IsActive { get; set; }

        public bool IsRemoved { get; set; } = false;
   
}
}