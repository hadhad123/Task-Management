using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class TaskConfiguration : EntityBaseConfiguration<Model.Task>
    {
        public TaskConfiguration()
        {
            Property(t => t.Description).IsRequired().HasMaxLength(250);
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.AssignedTo).IsRequired();
            Property(t => t.StatusID).IsRequired();
        }
    }
}
