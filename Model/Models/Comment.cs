using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Comment : IEntityBase
    {
        public int ID { get; set; }
        public string CommentDescription { get; set; }
        public int UserID { get; set; }
        public int TaskID { get; set; }
        public User User { get; set; }
        public Task Task { get; set; }
    }
}
