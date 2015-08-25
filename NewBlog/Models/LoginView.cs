using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewBlog.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name="UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name="Password")]
        public string Password { get; set; }


    }
}