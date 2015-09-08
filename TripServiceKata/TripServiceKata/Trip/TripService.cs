﻿using System.Collections.Generic;
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
            if (loggedUser != null)
            {
                if (user.HasFriend(loggedUser))
                    return tripRepository.FindTripsByUser(user);
                return new List<Trip>();
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}
