using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auth.Models
{
    public class Pizza
    {
       [Key]
      
       public int id { get; set; }
        [Display(Name ="Name")]
        [Required]
      public  string name { get; set; }
        [Display(Name = "Description")]
        [Required]
        public  string description { get; set; }

        [Display(Name = "Category")]
        [Required]
        public string category { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        public string imageUrl { get; set; }

    }

    public enum cat
    {
        Veg,
        NonVeg,
    }

}