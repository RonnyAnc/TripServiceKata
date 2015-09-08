using FluentAssertions;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        [Test]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public static void throw_an_exception_when_no_logged_user()
        {
            var service = new TestableTripService();
            service.GetTripsByUser(new User.User());
        }

        private class TestableTripService : TripService
        {
            protected override User.User GetLoggedUser()
            {
                return null;
            }
        }
    }
}
