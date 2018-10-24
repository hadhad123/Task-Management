namespace Data.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.TaskManagmentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.TaskManagmentContext context)
        {
            //  This method will be called after migrating to the latest version.
            AddRoles(context);
            AddUsers(context);
        }

        private void AddRoles(Data.TaskManagmentContext context)
        {
            List<Role> Roles = new List<Role>()
            {
                new Role()
                {
                    RoleName = "Manager"
                },
                new Role()
                {
                    RoleName = "Employee"
                }
            };

            foreach(Role role in Roles)
            {
                Role RoleExists = context.Roles.FirstOrDefault(r => r.RoleName == role.RoleName);
                if (RoleExists == null)
                    context.Roles.Add(role);
            }
            context.Commit();
        }

        private void AddUsers(Data.TaskManagmentContext context)
        {
            Role ManagerRole = context.Roles.FirstOrDefault(r => r.RoleName == "Manager");
            User AdminUser = new User()
            {
                UserName = "Admin",
                HashedPassword = "AIR7kPNLtWC+BYxdigiXQaNTvTsz91WME8HWGBJxgePfyBNo+rpHYuCVtYLEwoikLg==",
                Email = "admin@gmail.com",
                Salt = "d85bdd0d-890c-46d3-8dc0-b5491c33f099",
                RoleID = ManagerRole.ID,
                Active = true
            };
            User UserExists = context.Users.FirstOrDefault(u => u.UserName == AdminUser.UserName);
            if (UserExists == null)
            {
                context.Users.Add(AdminUser);
                context.Commit();
            }
                
           
        }
    }
}
