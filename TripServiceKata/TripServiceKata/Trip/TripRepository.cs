using System.Collections.Generic;

namespace TripServiceKata.Trip
{
    public class TripRepository
    {
        public virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}