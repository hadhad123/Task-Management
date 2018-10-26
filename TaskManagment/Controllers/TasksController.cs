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
            IEnumerable<TaskViewModel> viewModelTask;
            IEnumerable<Task> Tasks;

            Tasks = TaskService.GetTasks();
            viewModelTask = Mapper.Map<IEnumerable<Task>, IEnumerable<TaskViewModel>>(Tasks);
            return View(viewModelTask);
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
        public ActionResult Create(TaskViewModel TaskView, int? ID)
        {
            if (ModelState.IsValid)
            {
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

    }
}