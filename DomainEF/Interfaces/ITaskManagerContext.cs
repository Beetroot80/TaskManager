using System.Data.Entity;
using DomainCore;

namespace DomainEF.Interfaces
{
    public interface ITaskManagerContext : IContext
    {
        DbSet<ClientProfile> ClientProfiles { get; set; }
        //DbSet<User> Users { get; set; }
        DbSet<PersonalInfo> PersonalInfo { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Status> Status { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<DomainTask> Tasks { get; set; }
    }
}
