namespace Findmaster.DataAccessLayer.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }

        public byte[] UserPasswordHash { get; set; }

        public byte[] UserPasswordSalt { get; set; }
    }
}
