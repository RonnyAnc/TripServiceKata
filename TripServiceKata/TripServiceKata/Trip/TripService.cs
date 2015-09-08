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
            var loggedUser = loggedUserService.GetUser();
            if (loggedUser == null) throw new UserNotLoggedInException();

            return user.HasFriend(loggedUser) ? tripRepository.FindTripsByUser(user) : new List<Trip>();
        }
    }
}
