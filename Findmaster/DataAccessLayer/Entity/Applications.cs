using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class Applications
    {
        public Applications(int userId, int vacancyId)
        {
            UserId = userId;
            VacancyId = vacancyId;
        }

        public int ApplicationsId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; }
    }
}
