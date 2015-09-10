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
        private SessionService _sessionService;
        private TripRepository tripRepo;

        [SetUp]
        public void SetUp()
        {
            loggedUser = new User.User();
            anyUser = new User.User();
            _sessionService = Substitute.For<SessionService>();
            tripRepo = Substitute.For<TripRepository>();
        }

        [TestFixture]
        private class WhenNoLoggedUser : TripServiceShould
        {
            [Test]
            [ExpectedException(typeof(UserNotLoggedInException))]
            public void throw_an_exception()
            {
                _sessionService.GetLoggedUser().Returns(new UserSearchResult(null));
                var service = new TripService(_sessionService, tripRepo);
                service.GetTripsByUser(anyUser);
            }
        }

        [TestFixture]
        public class WhenThereIsLoggedUser : TripServiceShould
        {
            [SetUp]
            public void Stub()
            {
                _sessionService.GetLoggedUser().Returns(new UserSearchResult(loggedUser));
            }

            [Test]
            public void and_he_is_not_friend_of_required_user_get_an_empty_list()
            {
                var service = new TripService(_sessionService, tripRepo);
                var trips = service.GetTripsByUser(anyUser);
                trips.Should().BeEmpty();
            }

            [Test]
            public void and_he_is_friend_of_required_user_get_his_trips()
            {
                tripRepo.FindTripsByUser(anyUser).Returns(new List<Trip.Trip> { new Trip.Trip() });
                anyUser.AddFriend(loggedUser);
                var service = new TripService(_sessionService, tripRepo);

                var trips = service.GetTripsByUser(anyUser);

                trips.Count.Should().Be(1);
            }
        }
    }
}
