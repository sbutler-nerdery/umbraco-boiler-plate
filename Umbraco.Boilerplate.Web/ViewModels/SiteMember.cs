using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Umbraco.Boilerplate.Web.ViewModels
{
    public class SiteMember : IValidatableObject
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(OldPassword))
            {
                if (string.IsNullOrEmpty(NewPassword))
                {
                    yield return new ValidationResult("New password cannot be blank");
                }
                else if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    yield return new ValidationResult("The the password and confirm password must be the same.");
                }
                else if (NewPassword.Equals(OldPassword))
                {
                    yield return new ValidationResult("The old and new password must be different");
                }
                else if (!NewPassword.Equals(ConfirmPassword))
                {
                    yield return new ValidationResult("The the password and confirm password must be the same.");
                }
            }
        }
    }
}