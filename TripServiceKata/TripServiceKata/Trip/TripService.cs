using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly LoggedUserService loggedUserService;
        private readonly TripRepository tripRepository;

        public TripService(LoggedUserService loggedUserService, TripRepository tripRepository)
        {
            this.loggedUserService = loggedUserService;
            this.tripRepository = tripRepository;
        }

        public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
            User.User loggedUser = GetLoggedUser();
            bool isFriend = false;
            if (loggedUser != null)
            {
                foreach(User.User friend in user.GetFriends())
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }
                if (isFriend)
                {
                    tripList = FindTripsByUser(user);
                }
                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return tripRepository.FindTripsByUser(user);
        }

        protected virtual User.User GetLoggedUser()
        {
            return loggedUserService.GetUser();
        }
    }
}
