using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        TaskManagmentContext dbContext;

        public TaskManagmentContext Init()
        {
            return dbContext ?? (dbContext = new TaskManagmentContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }

}
