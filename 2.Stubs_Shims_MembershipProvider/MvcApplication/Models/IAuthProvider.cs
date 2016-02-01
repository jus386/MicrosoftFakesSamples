namespace MyMvcApplication.Models
{
    public interface IAuthProvider
    {
        bool ValidateUser(string email, string password);

        void SetAuthCookie(string email, bool rememberMe);
    }
}
