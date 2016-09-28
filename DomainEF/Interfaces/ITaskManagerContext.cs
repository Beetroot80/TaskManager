using System;
using System.Data.Entity;

using DomainEntities;

namespace DomainEF.Interfaces
{
    public interface ITaskManagerContext : IDisposable
    {
        DbSet<ClientProfile> ClientProfiles { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Status> Status { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<DomainTask> Tasks { get; set; }
        int SaveChanges();
    }
}
