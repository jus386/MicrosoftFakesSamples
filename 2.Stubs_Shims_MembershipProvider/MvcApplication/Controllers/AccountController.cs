using System.Web.Mvc;
using MyMvcApplication.Models;
using System.Web.Security;

namespace MyMvcApplication.Controllers
{
    /// <summary>
    /// MVC controller that is difficult to test due to the dependency of the Membership and FormsAuthentication
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("auth", "The user name or password incorrect.");
            }
            return View(model);
        }
    }
}