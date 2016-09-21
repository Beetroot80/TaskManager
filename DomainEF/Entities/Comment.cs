namespace DomainCore
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        //Foreign keys
        public int DomainTaskId { get; set; }
        public string ClientProfileId { get; set; }

        //Navigation properties
        public DomainTask DomainTask { get; set; }
        public ClientProfile ClientProfile { get; set; }
    }
}
