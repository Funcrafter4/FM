using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class Favourite
    {
        public Favourite(int userId, int vacancyId)
        {
            UserId = userId;
            VacancyId = vacancyId;
        }

        public int FavouriteId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; }


    }
}
