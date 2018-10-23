using AutoMapper;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagment.ViewModels;

namespace TaskManagment.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(UserService UserService)
        {
            this.userService = UserService;
        }

        // GET: Users
        public ActionResult Index(string category = null)
        {
            IEnumerable<UserViewModel> viewModelUser;
            IEnumerable<User> Users;

            Users = userService.GetUser();
            viewModelUser = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(Users);

            return View(viewModelUser);
        }
    }
}