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
        private static readonly User.User LoggedUser;

        static TripServiceShould()
        {
            LoggedUser = new User.User();
        }

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
            var service = new TestableTripService(LoggedUser);
            var trips = service.GetTripsByUser(new User.User());
            trips.Should().BeEmpty();
        }

        [Test]
        public static void get_trips_of_required_user_when_logged_user_is_his_friend()
        {
            var anyUser = new User.User();
            anyUser.AddFriend(LoggedUser);
            anyUser.AddTrip(new Trip.Trip());
            var service = new TestableTripService(LoggedUser);

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
