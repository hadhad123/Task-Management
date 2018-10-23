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
        List<User> GetUser();
        void CreateUser(User User);
        void SaveUser();
    }
}
