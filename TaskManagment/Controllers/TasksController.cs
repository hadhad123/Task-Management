using AutoMapper;
using Model;
using Model.ViewModels;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagment.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService TaskService;
        private readonly IUserService UserService;
        private readonly ITaskStatusService TaskStatusService;

        public TasksController(ITaskService TaskService, IUserService UserService, ITaskStatusService TaskStatusService)
        {
            this.TaskService = TaskService;
            this.UserService = UserService;
            this.TaskStatusService = TaskStatusService;
        }
        // GET: Tasks
        public ActionResult Index()

        {
            IEnumerable<TaskViewModel> TaskViews = TaskService.GetTasks();
            return View(TaskViews);
        }

        public ActionResult Create(int? ID)
        {
            List<User> Users = UserService.GetUsers();
            ViewBag.UsersList = new SelectList(Users, "ID", "UserName");

            List<TaskStatus> TaskStatuses = TaskStatusService.GetTaskStatuses();
            ViewBag.TaskStatusesList = new SelectList(TaskStatuses, "ID", "Status");

            if (ID != null) //edit
            {
                TaskViewModel Task = TaskService.GetTaskByID(ID ?? 0);
                return View(Task);
            }
             return View();

        }

        // POST: TaskViewModels/Create/ID
        [HttpPost]
        public ActionResult Create(TaskViewModel TaskView, int? ID, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //upload File
                if (file !=null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    file.SaveAs(path);
                    TaskView.File = path;
                }

                if (ID == null) //add
                {
                    Task Task;
                    Task = Mapper.Map<TaskViewModel, Task>(TaskView);
                    TaskService.CreateTask(Task);
                }
                else //update
                {
                    Task Task = Mapper.Map<TaskViewModel, Task>(TaskView);
                    TaskService.EditTask(Task);
                }

                    return RedirectToAction("Index");
            }

            return View(TaskView);
        }

        [HttpGet]
        public ActionResult Close (int ID)
        {
            IEnumerable<TaskViewModel> TaskViews = TaskService.CloseTask(ID);
            return View("Index", TaskViews);
        }

        [HttpPost]
        public List<TaskViewModel> AddComment(CommentView NewComment)
        {
            IEnumerable<TaskViewModel> TaskViews = TaskService.AddComment(NewComment);
            return TaskViews.ToList();
        }

        [HttpGet]
        public ActionResult DownloadFile(int ID)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
            TaskViewModel Task = TaskService.GetTaskByID(ID);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + Task.File);
            string fileName = Task.File;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Details(int ID)
        {
            TaskViewModel Task = TaskService.GetTaskByID(ID);
            return View("Details", Task);
        }

    }
}