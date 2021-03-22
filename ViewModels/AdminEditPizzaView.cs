using Auth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auth.ViewModels
{
    public class AdminEditPizzaView
    {
       
        public Pizza pizza { get; set; }

        public List<Size> size { get; set; }
    }
}