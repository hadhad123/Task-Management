using Data.Infrastructure;
using Data.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository RoleRepository;
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IRoleRepository RoleRepository, IUnitOfWork unitOfWork)
        {
            this.RoleRepository = RoleRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Role> GetRoles()
        {
            List<Role> Roles = RoleRepository.GetAll().ToList();
            return Roles;
        }

        public Role GetRoleByID(int ID)
        {
            Role Role = RoleRepository.GetByID(ID);
            return Role;
        }

        public void CreateRole(Role Role)
        {
            Role Exists = RoleRepository.Get(x => x.RoleName == Role.RoleName).FirstOrDefault();
            if (Exists != null)
            {
                throw new Exception("Role already exists");
            }

            RoleRepository.Add(Role);
            SaveRole();
        }

        public void EditRole(Role Role)
        {
            RoleRepository.Edit(Role.ID,Role);
            SaveRole();
        }

        public void SaveRole()
        {
            unitOfWork.Commit();
        }


    }
}
