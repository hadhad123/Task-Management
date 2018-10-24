using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserByID(int ID);
        void CreateUser(User User);
        void EditUser(User User);
        void SaveUser();
    }
}
