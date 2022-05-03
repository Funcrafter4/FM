using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class Favourite
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; }


    }
}
