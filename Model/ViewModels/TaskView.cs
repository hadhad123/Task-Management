using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model.ViewModels
{
    public class TaskViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TaskStatusID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int AssignedUserID { get; set; }
        public string File { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public List<Comment> Comments{ get; set; }
        //public List<Comment> ChildComments { get; set; }
        public User User { get; set; }
        public User AssignedUser { get; set; }


        public TaskViewModel()
        {
            Comments = new List<Comment>();
        }
    }
}