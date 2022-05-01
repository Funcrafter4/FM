namespace Findmaster.DataAccessLayer.Entity
{
    public class Messages
    {
        public int MessagesId { get; set; }

        public string Message { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public Vacancy Vacancy  { get; set; }
    }
}
