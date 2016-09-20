namespace DomainCore
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Foreign keys
        public int? UserId { get; set; }

        //Navigation properties
        public User User { get; set; }
    }
}
