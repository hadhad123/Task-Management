using AutoMapper;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagment.ViewModels;

namespace TaskManagment.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UsersController(IUserService UserService, IRoleService roleService)
        {
            this.userService = UserService;
            this.roleService = roleService;
        }

        // GET: Users
        public ActionResult Index(string category = null)
        {
            IEnumerable<UserViewModel> viewModelUser;
            IEnumerable<User> Users;

            Users = userService.GetUsers();
            viewModelUser = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(Users);

            return View(viewModelUser);
        }

        // edit
        public ActionResult Create(int? ID)
        {
            if(ID != null) //edit
            {
                User User = userService.GetUserByID(ID??0);
                UserViewModel UserView = Mapper.Map<User, UserViewModel>(User);

                List<Role> Roles = roleService.GetRoles();
                ViewBag.RolesList = new SelectList(Roles, "ID", "RoleName");

                return View(UserView);
            }
            else
            {
                List<Role> Roles = roleService.GetRoles();

                ViewBag.RolesList = new SelectList(Roles, "ID", "RoleName");
                return View();
            }

        }

        // POST: UserViewModels/Create/ID
        [HttpPost]
        public ActionResult Create(UserViewModel UserView, int?ID)
        {
            if (ModelState.IsValid)
            {
                if(ID ==null) //add
                {
                    User User;
                    User = Mapper.Map<UserViewModel, User>(UserView);
                    userService.CreateUser(User);
                }
                else //update
                {
                    User User = Mapper.Map<UserViewModel, User>(UserView);
                    userService.EditUser(User);
                }
               

                return RedirectToAction("Index");
            }
              
            return View(UserView);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int ID)
        {
            User User = userService.GetUserByID(ID);
            UserViewModel UserView = Mapper.Map<User, UserViewModel>(User);

            List<Role> Roles = roleService.GetRoles();
            ViewBag.RolesList = new SelectList(Roles, "ID", "RoleName");

            return View("Create",UserView);
        }

        //POST: Users/Update/5
        [HttpPost]
        public ActionResult Update(UserViewModel UserView)
        {
            User User = Mapper.Map<UserViewModel, User>(UserView);
            userService.EditUser(User);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeactivatePartialView(int ID)
        {
            User User = userService.GetUserByID(ID);
            UserViewModel viewModelUser = Mapper.Map<User, UserViewModel>(User);
            return PartialView(viewModelUser);
        }

        [HttpGet]
        public ActionResult DeactivateUser(int ID)
        {
            userService.DeactivateUser(ID);
            IEnumerable<UserViewModel> viewModelUser;
            IEnumerable<User> Users;

            Users = userService.GetUsers();
            viewModelUser = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(Users);
            return View("Index", viewModelUser);
        }
    }
}