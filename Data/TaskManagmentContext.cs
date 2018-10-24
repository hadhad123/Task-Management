using Data.Configurations;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TaskManagmentContext : DbContext //main class for accessing data from the database
    {
        public TaskManagmentContext() : base("TaskManagmentContext")
        {
            //Database.SetInitializer<TaskManagmentContext>(null);
        }

        #region Entity Sets
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<Model.Task> Tasks { get; set; }
        public IDbSet<Model.TaskStatus> TaskStatuses { get; set; }
        public IDbSet<Comment> Comments { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new TaskConfiguration());
            modelBuilder.Configurations.Add(new TaskStatusConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
        }

    }
}
