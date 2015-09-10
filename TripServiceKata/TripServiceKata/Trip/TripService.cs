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
            var userSearchResult = loggedUserService.GetUser();
            if (userSearchResult.HasNotUser()) throw new UserNotLoggedInException();

            return user.HasFriend(userSearchResult.User()) ? tripRepository.FindTripsByUser(user) : new List<Trip>();
        }
    }
}
