namespace DomainEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int DomainTaskId { get; set; }
        public string ClientId { get; set; }

        public virtual DomainTask DomainTask { get; set; }
        public virtual ApplicationUser Client { get; set; }
    }
}
