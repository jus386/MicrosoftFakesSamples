using System.Web.Security;

namespace MyMvcApplication.Models
{
    public class WebAuthProvider : IAuthProvider
    {
        public void SetAuthCookie(string email, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(email, rememberMe);
        }

        public bool ValidateUser(string email, string password)
        {
            return Membership.ValidateUser(email, password);
        }
    }
}