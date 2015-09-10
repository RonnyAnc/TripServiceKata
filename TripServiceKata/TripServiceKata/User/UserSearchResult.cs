namespace TripServiceKata.User
{
    public class UserSearchResult
    {
        private readonly User possibleUser;

        public UserSearchResult(User possibleUser)
        {
            this.possibleUser = possibleUser;
        }

        public bool HasNotUser()
        {
            return possibleUser == null;
        }

        public User User()
        {
            if (possibleUser == null) throw new UserValueDoesNotExist();
            return possibleUser;
        }
    }

    public class UserValueDoesNotExist : System.Exception
    {
    }
}