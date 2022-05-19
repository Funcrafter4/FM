using System.ComponentModel.DataAnnotations.Schema;

namespace Findmaster.DataAccessLayer.Entity
{
    public class Messages
    {
        public Messages(string message, int fromUserId, int toUserId, int vacancyId)
        {
            Message = message;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            VacancyId = vacancyId;
        }

        public int MessagesId { get; set; }

        public string Message { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; }
    }
}
