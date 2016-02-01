using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMvcApplication.Models;
using System.Web.Mvc;

namespace MyMvcApplication.Controllers.Tests
{
    /// <summary>
    /// Unit tests using MSFakes shims. Note that we are testing controller class with dependencies to Membership and FormsAuthentication.
    /// </summary>
    [TestClass()]
    public class AccountControllerTestSh
    {

        [TestMethod()]
        public void Login_ValidCredentials_AuthenticateAndRedirect()
        {
            // arrange
            var model = new LoginViewModel { Password = "123", Email = "usrtest@mail.com", RememberMe = true };
            var returnUrl = "/home/index";
            bool calledValidateUser = false;
            bool calledSetCookie = false;
            using(ShimsContext.Create())
            {
                System.Web.Security.Fakes.ShimMembership.ValidateUserStringString = (usr, pwd) =>
                {
                    calledValidateUser = true;
                    return usr == "usrtest@mail.com" && pwd == "123";
                };

                System.Web.Security.Fakes.ShimFormsAuthentication.SetAuthCookieStringBoolean = (email, remeberMe) =>
                {
                    calledSetCookie = true;
                };

                // act
                AccountController controller = new AccountController();
                var redirectResult = controller.Login(model, returnUrl) as RedirectResult;

                // assert
                Assert.IsTrue(calledValidateUser, "Membership.ValidateUser not invoked");
                Assert.IsTrue(calledSetCookie, "FormsAuthentication.SetAuthCookie not invoked");
                Assert.AreEqual(returnUrl, redirectResult.Url);
            }
        }

        [TestMethod()]
        public void Login_InvalidCredentials_ErrorMessage()
        {
            // arrange
            var model = new LoginViewModel { Password = "wrong", Email = "wrong@mail.com", RememberMe = false };
            var returnUrl = "/home/index";
            using (ShimsContext.Create())
            {
                System.Web.Security.Fakes.ShimMembership.ValidateUserStringString = (usr, pwd) =>
                {
                    return usr == "usrtest@mail.com" && pwd == "123";
                };

                // act
                AccountController controller = new AccountController();
                var viewResult = controller.Login(model, returnUrl) as ViewResult;
                
                // assert
                Assert.AreEqual(viewResult.ViewName, string.Empty);
                Assert.AreEqual(controller.ModelState["auth"].Errors[0].ErrorMessage, "The user name or password incorrect.", "Incorrect error message");
            }
        }

    }
}