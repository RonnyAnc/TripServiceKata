using TripServiceKata.Exception;

namespace TripServiceKata.User
{
    public class SessionService
    {
        public virtual UserSearchResult GetUser()
        {
            var possibleUser = UserSession.GetInstance().GetLoggedUser();
            return new UserSearchResult(possibleUser);
        }
    }

}