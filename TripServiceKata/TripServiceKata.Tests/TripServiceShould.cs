using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private User.User loggedUser;
        private User.User anyUser;

        [SetUp]
        public void SetUp()
        {
            loggedUser = new User.User();
            anyUser = new User.User();
        }

        [Test]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void throw_an_exception_when_no_logged_user()
        {
            var loggedUserService = Substitute.For<LoggedUserService>();
            var tripRepo = Substitute.For<TripRepository>();
            var service = new TripService(loggedUserService, tripRepo);
            service.GetTripsByUser(anyUser);
        }

        [Test]
        public void get_an_empty_list_when_logged_user_is_not_friend_of_required_user()
        {
            var loggedUserService = Substitute.For<LoggedUserService>();
            loggedUserService.GetUser().Returns(loggedUser);
            var tripRepo = Substitute.For<TripRepository>();
            var service = new TripService(loggedUserService, tripRepo);
            var trips = service.GetTripsByUser(anyUser);
            trips.Should().BeEmpty();
        }

        [Test]
        public void get_trips_of_required_user_when_logged_user_is_his_friend()
        {
            var tripRepo = Substitute.For<TripRepository>();
            tripRepo.FindTripsByUser(anyUser).Returns(new List<Trip.Trip> {new Trip.Trip()});
            var loggedUserService = Substitute.For<LoggedUserService>();
            loggedUserService.GetUser().Returns(loggedUser);
            anyUser.AddFriend(loggedUser);
            var service = new TripService(loggedUserService, tripRepo);

            var trips = service.GetTripsByUser(anyUser);

            trips.Count.Should().Be(1);
        }
    }
}
