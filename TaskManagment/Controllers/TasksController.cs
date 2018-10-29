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
using TaskManagment.ViewModels;

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
            IEnumerable<TaskView> TaskViews = TaskService.GetTasks();
            IEnumerable<TaskViewModel> Tasks = Mapper.Map<IEnumerable<TaskView>, IEnumerable<ViewModels.TaskViewModel>>(TaskViews);
            return View(Tasks);
        }

        public ActionResult Create(int? ID)
        {
            List<User> Users = UserService.GetUsers();
            ViewBag.UsersList = new SelectList(Users, "ID", "UserName");

            List<TaskStatus> TaskStatuses = TaskStatusService.GetTaskStatuses();
            ViewBag.TaskStatusesList = new SelectList(TaskStatuses, "ID", "Status");

            if (ID != null) //edit
            {
                Task Task = TaskService.GetTaskByID(ID ?? 0);
                TaskViewModel TaskView = Mapper.Map<Task, TaskViewModel>(Task);
                return View(TaskView);
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
            IEnumerable<TaskView> TaskViews = TaskService.CloseTask(ID);
            IEnumerable<TaskViewModel> Tasks = Mapper.Map<IEnumerable<TaskView>, IEnumerable<ViewModels.TaskViewModel>>(TaskViews);
            return View("Index",Tasks);
        }

        [HttpPost]
        public List<TaskViewModel> AddComment(CommentView NewComment)
        {
            IEnumerable<TaskView> TaskViews = TaskService.AddComment(NewComment);
            IEnumerable<TaskViewModel> Tasks = Mapper.Map<IEnumerable<TaskView>, IEnumerable<ViewModels.TaskViewModel>>(TaskViews);
            return Tasks.ToList();
        }

        [HttpGet]
        public ActionResult DownloadFile(int ID)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
            Task Task = TaskService.GetTaskByID(ID);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + Task.File);
            string fileName = Task.File;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}