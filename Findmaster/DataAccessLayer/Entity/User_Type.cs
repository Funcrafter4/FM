using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class User_Type
    {
        [ForeignKey("userId")]
        public int UserId { get; set; }
        public int UserType { get; set; }
    }
}
