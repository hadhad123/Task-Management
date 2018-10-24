using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Role: IEntityBase
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
