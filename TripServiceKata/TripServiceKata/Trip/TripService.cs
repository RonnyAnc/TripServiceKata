using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        protected readonly LoggedUserService LoggedUserService;
        private readonly TripRepository tripRepository;

        public TripService(LoggedUserService loggedUserService)
        {
            this.LoggedUserService = loggedUserService;
        }

        protected TripService()
        {
        }

        public TripService(LoggedUserService loggedUserService, TripRepository tripRepository)
        {
            LoggedUserService = loggedUserService;
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
            return LoggedUserService.GetUser();
        }
    }
}
