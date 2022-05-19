namespace Findmaster.DataAccessLayer.DTO
{
    public class UserDTO
    {
        public UserDTO(int userId, string userName, string userSurname,string userEmail)
        {
            UserId = userId;
            UserName = userName;
            UserSurname = userSurname;
            UserEmail = userEmail;
        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string UserEmail { get; set; }
    }
}
