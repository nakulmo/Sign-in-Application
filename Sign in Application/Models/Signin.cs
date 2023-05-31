using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Sign_in_Application.Models
{
    public class Signin
    {
        [Display(Name = "User id")]
        public int Id { get; set; }

        [Display(Name = "User name")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

       

    }
}