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
        public static void get_an_empty_list_when_logged_user_is_not_friend_of_passed_user()
        {
            var loggedUser = new User.User();
            var service = new TestableTripService(loggedUser);
            var trips = service.GetTripsByUser(new User.User());
            trips.Should().BeEmpty();
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
        }
    }
}
