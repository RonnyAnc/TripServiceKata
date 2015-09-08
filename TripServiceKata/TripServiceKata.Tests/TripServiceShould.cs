using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        [Test]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public static void throw_an_exception_when_no_logged_user()
        {
            var service = new TestableTripService(null);
            service.GetTripsByUser(new User.User());
        }

        [Test]
        public static void get_an_empty_list_when_logged_user_is_not_friend_of_required_user()
        {
            var loggedUser = new User.User();
            var service = new TestableTripService(loggedUser);
            var trips = service.GetTripsByUser(new User.User());
            trips.Should().BeEmpty();
        }

        [Test]
        public static void get_trips_of_required_user_when_logged_user_is_his_friend()
        {
            var loggedUser = new User.User();
            var anyUser = new User.User();
            anyUser.AddFriend(loggedUser);
            anyUser.AddTrip(new Trip.Trip());
            var service = new TestableTripService(loggedUser);

            var trips = service.GetTripsByUser(anyUser);

            trips.Count.Should().Be(1);
        }

        private class TestableTripService : TripService
        {
            private User.User loggedUser;

            public TestableTripService(User.User loggedUser)
            {
                this.loggedUser = loggedUser;
            }

            protected override User.User GetLoggedUser()
            {
                return loggedUser;
            }

            protected override List<Trip.Trip> FindTripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
