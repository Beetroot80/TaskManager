using System;
using System.Data.Entity;
using DomainCore;

namespace DomainEF
{
    public interface ITaskManagerContext : IContext
    {
        DbSet<User> Users { get; set; }
        DbSet<PersonalInfo> PersonalInfo { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Status> Status { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<DomainTask> Tasks { get; set; }
    }
    public class TaskManagerContext : DbContext, ITaskManagerContext
    { 
        public TaskManagerContext()
            : base("TaskManagerDB")
        {
            Database.SetInitializer<TaskManagerContext>(new TaskManagerInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(x => x.PersonalInfo).WithRequiredDependent(x => x.User);
            modelBuilder.Entity<User>().HasMany(x => x.DomainTasks).WithRequired(x => x.CreatedBy).WillCascadeOnDelete(false);
            //modelBuilder.Entity<DomainTask>().HasRequired(x => x.CreatedBy);
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void SetAdded(object entity)
        {
            Entry(entity).State = EntityState.Added;
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