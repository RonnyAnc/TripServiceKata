using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly SessionService sessionService;
        private readonly TripRepository tripRepository;

        public TripService(SessionService sessionService, TripRepository tripRepository)
        {
            this.sessionService = sessionService;
            this.tripRepository = tripRepository;
        }

        public List<Trip> GetTripsByUser(User.User user)
        {
            var userSearchResult = sessionService.GetLoggedUser();
            if (userSearchResult.HasNotUser()) throw new UserNotLoggedInException();

            return user.HasFriend(userSearchResult.User()) ? tripRepository.FindTripsByUser(user) : new List<Trip>();
        }
    }
}
