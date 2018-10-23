using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UserRole : IEntityBase
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}
