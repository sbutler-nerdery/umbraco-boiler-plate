using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Boilerplate.Web.ViewModels;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;

namespace Umbraco.Boilerplate.Web.Controllers
{
    public class AccountController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Login()
        {
            var model = new Login();
            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitLogin(Login model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Membership.Provider.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.KeepMeLoggedIn);

                        //If the referring URL is the login page, then redirect the user to the my account page. Otherwise,
                        //send them back to the referrer
                        var redirectUrl = Request.UrlReferrer.AbsoluteUri.Contains("/login/") ? "/my-account/" : Request.UrlReferrer.AbsoluteUri;
                        return Redirect(redirectUrl);
                    }

                    ModelState.AddModelError("BadCredentials", "Bad user name or password.");
                }
                catch (Exception ex)
                {
                    LogHelper.Error(this.GetType(), "Login Error", ex);
                    ModelState.AddModelError("Error", "We are sorry, but the system encountered an error. Please try again.");
                }
            }

            return CurrentUmbracoPage();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [ChildActionOnly]
        public ActionResult Register()
        {
            var model = new Register();
            return View(model);
        }

        public ActionResult NewUser(Register model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = Membership.CreateUser(model.UserName, model.Password, model.Email);
                    System.Web.Security.Roles.AddUserToRole(member.UserName, "DefaultUsers");
                    FormsAuthentication.SetAuthCookie(member.UserName, true);
                    TempData.Add("Success", "Congradulations! Your use account has been successfully created.");
                    return Redirect("/my-account/");
                }
                catch (Exception ex)
                {
                    LogHelper.Error(this.GetType(), "Account Creation Error", ex);
                    ModelState.AddModelError("Error", ex.Message);
                }
            }

            return CurrentUmbracoPage();
        }

        [ChildActionOnly]
        public ActionResult MyAccount()
        {
            var member = Membership.GetUser(User.Identity.Name);
            var model = new SiteMember { 
                UserName = member.UserName,
                Email = member.Email
            };
            return View(model);
        }

        public ActionResult UpdateAccount(SiteMember model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatePasswordSuccessful = true;
                    var member = Membership.GetUser(model.UserName);
                    member.Email = model.Email;
                    Membership.UpdateUser(member);

                    if(!string.IsNullOrEmpty(model.NewPassword))
                    {
                        updatePasswordSuccessful = member.ChangePassword(model.OldPassword, model.NewPassword);
                    }

                    if (!updatePasswordSuccessful)
                    {
                        ModelState.AddModelError("Error", "The old password was not recognized.");
                    }
                    else
                    {
                       //This is the collection that we use to pass data back 
                       //on redirect.
                       TempData.Add("Success", "Your account has been updated successfully.");
                    }

                    return RedirectToCurrentUmbracoPage();
                }
                catch (Exception ex)
                {
                    LogHelper.Error(this.GetType(), "Account Creation Error", ex);
                    ModelState.AddModelError("Error", "We are sorry, but the system encountered an error. Please try again.");
                }
            }

            return CurrentUmbracoPage();
        }
    }
}
