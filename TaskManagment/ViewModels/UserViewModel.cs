using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagment.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}