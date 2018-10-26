using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User: IEntityBase
    {
        public User()
        {
            Tasks = new List<Task>();
            CreatedTasks = new List<Task>();
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }
        public bool Active { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Task> CreatedTasks { get; set; }

    }
}
