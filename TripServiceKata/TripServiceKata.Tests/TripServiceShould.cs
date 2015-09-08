using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private User.User LoggedUser;
        private User.User AnyUser;

        [SetUp]
        public void SetUp()
        {
            LoggedUser = new User.User();
            AnyUser = new User.User();
        }

        [Test]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void throw_an_exception_when_no_logged_user()
        {
            var loggedUserService = Substitute.For<LoggedUserService>();
            var tripRepo = Substitute.For<TripRepository>();
            var service = new TripService(loggedUserService, tripRepo);
            service.GetTripsByUser(AnyUser);
        }

        [Test]
        public void get_an_empty_list_when_logged_user_is_not_friend_of_required_user()
        {
            var loggedUserService = Substitute.For<LoggedUserService>();
            loggedUserService.GetUser().Returns(LoggedUser);
            var tripRepo = Substitute.For<TripRepository>();
            var service = new TripService(loggedUserService, tripRepo);
            var trips = service.GetTripsByUser(AnyUser);
            trips.Should().BeEmpty();
        }

        [Test]
        public void get_trips_of_required_user_when_logged_user_is_his_friend()
        {
            var tripRepo = Substitute.For<TripRepository>();
            tripRepo.FindTripsByUser(AnyUser).Returns(new List<Trip.Trip> {new Trip.Trip()});
            var loggedUserService = Substitute.For<LoggedUserService>();
            loggedUserService.GetUser().Returns(LoggedUser);
            AnyUser.AddFriend(LoggedUser);
            var service = new TripService(loggedUserService, tripRepo);

            var trips = service.GetTripsByUser(AnyUser);

            trips.Count.Should().Be(1);
        }

        [Test]
        public void extracting_TripRepository_collaboration()
        {
            var tripRepo = Substitute.For<TripRepository>();
            tripRepo.FindTripsByUser(AnyUser).Returns(new List<Trip.Trip> { new Trip.Trip() });
            var loggedUserService = Substitute.For<LoggedUserService>();
            loggedUserService.GetUser().Returns(LoggedUser);
            AnyUser.AddFriend(LoggedUser);
            var service = new TripService(loggedUserService, tripRepo);

            var trips = service.GetTripsByUser(AnyUser);

            trips.Should().HaveCount(1);
        }

        private class TestableTripService : TripService
        {
            public TestableTripService(LoggedUserService loggedUserService) : base(loggedUserService)
            {
            }

            protected override User.User GetLoggedUser()
            {
                return LoggedUserService.GetUser();
            }

            protected override List<Trip.Trip> FindTripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }
}
