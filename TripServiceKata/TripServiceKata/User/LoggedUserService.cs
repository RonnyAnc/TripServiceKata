using TripServiceKata.User;

namespace TripServiceKata
{
    public class LoggedUserService
    {
        public virtual User.User GetUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}