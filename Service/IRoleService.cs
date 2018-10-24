using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role GetRoleByID(int ID);
        void CreateRole(Role Role);
        void EditRole(Role Role);
        void SaveRole();
    }
}
