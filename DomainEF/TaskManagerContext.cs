using System.Data.Entity;
using DomainCore;

namespace DomainEF
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext()
            : base("TaskManagerDB")
        {
            Database.SetInitializer<TaskManagerContext>(new TaskManagerInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(x => x.PersonalInfo).WithRequiredDependent(x => x.User);
            modelBuilder.Entity<DomainTask>().HasRequired(x => x.CreatedBy);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfo { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DomainTask> Tasks { get; set; }
    }

}