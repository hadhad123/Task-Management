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
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IEncryptionService EncryptionService;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository UserRepository, IEncryptionService EncryptionService, IUnitOfWork unitOfWork)
        {
            this.UserRepository = UserRepository;
            this.EncryptionService = EncryptionService;
            this.unitOfWork = unitOfWork;
        }

        public List<User> GetUsers()
        {
            List<User> Users = UserRepository.GetAll().ToList();
            return Users;
        }

        public User GetUserByID(int ID)
        {
            User User = UserRepository.GetByID(ID);
            return User;
        }

        public void CreateUser(User User)
        {
            User Exists = UserRepository.Get(x => x.UserName == User.UserName && x.Email == User.Email).FirstOrDefault();
            if(Exists != null)
            {
                throw new Exception("Username & Email already in use");
            }
            string salt = EncryptionService.CreateSalt();
            string HassedPassword = EncryptionService.EncryptPassword(User.HashedPassword, salt);
            User.HashedPassword = HassedPassword;
            User.Salt = salt;

            UserRepository.Add(User);
            SaveUser();
        }

        public void EditUser(User User)
        {
            User OldUserData = UserRepository.GetByID(User.ID);
            User.Salt = OldUserData.Salt;
            string NewHassedPassword = EncryptionService.EncryptPassword(User.HashedPassword, User.Salt);
            //check if password has been changed 
            if (OldUserData.HashedPassword != NewHassedPassword)
            {
                string salt = EncryptionService.CreateSalt();
                string HassedPassword = EncryptionService.EncryptPassword(User.HashedPassword, salt);
                User.HashedPassword = HassedPassword;
                User.Salt = salt;
            }
            User.Role = null;
            User.Tasks = null;
            
            UserRepository.Edit(User);
            SaveUser();
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }


    }
}
