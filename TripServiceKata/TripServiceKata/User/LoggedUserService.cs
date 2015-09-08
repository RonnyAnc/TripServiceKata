namespace TripServiceKata.User
{
    public class LoggedUserService
    {
        public virtual User GetUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}