using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class User_Type
    {
        public User_Type(bool userType)
        {
            UserType = userType;
        }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public bool UserType { get; set; }
    }
}
