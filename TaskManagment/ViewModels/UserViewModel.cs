using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagment.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string HashedPassword { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        public bool Active { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}