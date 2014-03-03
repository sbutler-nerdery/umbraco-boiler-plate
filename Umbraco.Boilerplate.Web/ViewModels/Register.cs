using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Umbraco.Boilerplate.Web.ViewModels
{
    public class Register
    {
        [Required]
        [Display(Name = "User name:")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}