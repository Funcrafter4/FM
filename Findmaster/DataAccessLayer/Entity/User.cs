namespace Findmaster.DataAccessLayer.Entity
{
    public class User
    {
        public User(string userEmail, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            UserEmail = userEmail;
            UserPasswordHash = userPasswordHash;
            UserPasswordSalt = userPasswordSalt;
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }

        public byte[] UserPasswordHash { get; set; }

        public byte[] UserPasswordSalt { get; set; }
    }
}
