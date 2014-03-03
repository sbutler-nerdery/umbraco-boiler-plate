using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Umbraco.Boilerplate.Web.ViewModels
{
    public class Login
    {
        [Required]
        [Display(Name="User name:")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Display(Name = "Keep me logged in")]
        public bool KeepMeLoggedIn { get; set; }
    }
}