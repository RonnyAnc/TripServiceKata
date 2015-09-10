using TripServiceKata.Exception;

namespace TripServiceKata.User
{
    public class LoggedUserService
    {
        public virtual UserSearchResult GetUser()
        {
            var possibleUser = UserSession.GetInstance().GetLoggedUser();
            return new UserSearchResult(possibleUser);
        }
    }

}