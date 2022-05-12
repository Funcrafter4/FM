using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class User_Info
    {
        public User_Info(string userName, string userSurname)
        {
            UserName = userName;
            UserSurname = userSurname;
        }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public string? UserSurname { get; set; }

        public string? UserNumber { get; set; }

        public string? UserAddress { get; set; }

        public string? UserBirthday { get; set; }

        public bool UserGender { get; set; }

        public string? UserSkills { get; set; }

        public string? UserWorkexp { get; set; }

    }
}
