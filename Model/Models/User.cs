using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User: IEntityBase
    {
        public User()
        {
            UserRoles = new List<UserRole>();
            Tasks = new List<Task>();
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

    }
}
