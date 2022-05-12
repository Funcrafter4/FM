using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class Applications
    {

        public int ApplicationsId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; }
    }
}
