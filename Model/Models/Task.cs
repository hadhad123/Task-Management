using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Task: IEntityBase
    {
        public Task()
        {
            Comments = new List<Comment>();
        }
        public int ID { get; set; }
        public string Description { get; set; }
        public int TaskStatusID { get; set; }
        public int UserID { get; set; }
        public int AssignedUserID { get; set; }
        public string File { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public User User { get; set; }
        public User AssignedUser { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
