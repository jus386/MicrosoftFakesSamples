using System.Web.Mvc;
using MyMvcApplication.Models;

namespace MyMvcApplication.Controllers
{
    /// <summary>
    /// Testable MVC controller. The dependencies are abstracted through IAuthProvider.
    /// </summary>
    [Authorize]
    public class AccountDController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountDController()
        {
            _authProvider = new WebAuthProvider();
        }

        /// <summary>
        /// Controller used for unit tests
        /// </summary>
        /// <param name="authProvider"></param>
        public AccountDController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.ValidateUser(model.Email, model.Password))
                {
                    _authProvider.SetAuthCookie(model.Email, model.RememberMe);
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "The user name or password incorrect.");
            }
            return View(model);
        }
    }
}