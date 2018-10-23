using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Task: IEntityBase
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
        public int AssignedTo { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public User CreatedUser { get; set; }
        public User AssignedUser { get; set; }
    }
}
