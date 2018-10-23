using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class CommentConfiguration : EntityBaseConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(c => c.CommentDescription).IsRequired().HasMaxLength(250);
            Property(c => c.TaskID).IsRequired();
            Property(c => c.UserID).IsRequired();
        }
    }
}
