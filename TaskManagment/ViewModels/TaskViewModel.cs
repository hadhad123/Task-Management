//using Model;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace TaskManagment.ViewModels
//{
//    public class TaskViewModel
//    {
//        public int ID { get; set; }
//        [Required]
//        public string Description { get; set; }
//        [Required]
//        public int TaskStatusID { get; set; }
//        [Required]
//        public int UserID { get; set; }
//        [Required]
//        public int AssignedUserID { get; set; }
//        public string File { get; set; }
//        public TaskStatus TaskStatus { get; set; }
//        public Comment ParentComment { get; set; }
//        public List<Comment> ChildComments { get; set; }
//        public User User { get; set; }
//        public User AssignedUser { get; set; }
//    }
//}