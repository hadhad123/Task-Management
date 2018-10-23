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
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository UserRepository, IUnitOfWork unitOfWork)
        {
            this.UserRepository = UserRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<User> GetUser()
        {
            var Users = UserRepository.GetAll().ToList();
            return Users;
        }

        public void CreateUser(User User)
        {

            //var existingUser = _userRepository.GetSingleByUsername(username);

            //if (existingUser != null)
            //{
            //    throw new Exception("Username is already in use");
            //}

            //var passwordSalt = _encryptionService.CreateSalt();

            //var user = new User()
            //{
            //    Username = username,
            //    Salt = passwordSalt,
            //    Email = email,
            //    IsLocked = false,
            //    HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
            //    DateCreated = DateTime.Now
            //};

            //_userRepository.Add(user);

            //_unitOfWork.Commit();

            //if (roles != null || roles.Length > 0)
            //{
            //    foreach (var role in roles)
            //    {
            //        addUserToRole(user, role);
            //    }
            //}

            //SaveUser();
            UserRepository.Add(User);
            SaveUser();
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }


    }
}
