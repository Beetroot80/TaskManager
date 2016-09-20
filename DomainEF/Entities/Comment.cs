namespace DomainCore
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        //Foreign keys
        public int DomainTaskId { get; set; }
        public int UserId { get; set; }

        //Navigation properties
        public DomainTask DomainTask { get; set; }
        public User User { get; set; }
    }
}
