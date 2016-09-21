using System;
using System.Data.Entity;
using DomainCore;
using DomainEF.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DomainEF
{
    public class TaskManagerContext : IdentityDbContext<ApplicationUser>, ITaskManagerContext
    {
        public TaskManagerContext()
            : base("TaskManagerDB")
        {
            Database.SetInitializer<TaskManagerContext>(new TaskManagerInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(x => x.PersonalInfo).WithRequiredDependent(x => x.User);
            modelBuilder.Entity<ClientProfile>().HasMany(x => x.DomainTasks).WithRequired(x => x.CreatedBy).WillCascadeOnDelete(false);
            modelBuilder.Entity<DomainTask>().Property(x => x.CreatedBy_Id).IsRequired();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void SetAdded(object entity)
        {
            Entry(entity).State = EntityState.Added;
        }

        public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfo { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DomainTask> Tasks { get; set; }
    }

}