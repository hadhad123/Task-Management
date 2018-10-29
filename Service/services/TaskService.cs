using Data.Infrastructure;
using Data.Repositories;
using Model;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository TaskRepository;
        private readonly ITaskStatusRepository TaskStatusRepository;
        private readonly ICommentRepository CommentRepository;
        private readonly IUnitOfWork unitOfWork;

        public TaskService(ITaskRepository TaskRepository, ITaskStatusRepository TaskStatusRepository, ICommentRepository CommentRepository,IUnitOfWork unitOfWork)
        {
            this.TaskRepository = TaskRepository;
            this.TaskStatusRepository = TaskStatusRepository;
            this.CommentRepository = CommentRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<TaskView> GetTasks()
        {
            List<string> Includes = new List<string>();
            Includes.Add("User");
            Includes.Add("AssignedUser");
            Includes.Add("TaskStatus");
            Includes.Add("Comments");

            List<Model.Task> Tasks = TaskRepository.GetAllWithIncludes(Includes).ToList();
            List<TaskView> TaskViews = new List<TaskView>();
         
            foreach(Model.Task tsk in Tasks)
            {
                TaskView NewTaskViews = new TaskView();
                NewTaskViews.ID = tsk.ID;
                NewTaskViews.Description = tsk.Description;
                NewTaskViews.TaskStatusID = tsk.TaskStatusID;
                NewTaskViews.UserID = tsk.UserID;
                NewTaskViews.AssignedUserID = tsk.AssignedUserID;
                NewTaskViews.TaskStatus = tsk.TaskStatus;
                NewTaskViews.ParentComment = tsk.Comments.Where(x => x.ParentCommentID == null).FirstOrDefault();
                NewTaskViews.ChildComments = tsk.Comments.Where(x=>x.ParentCommentID != null).ToList();
                NewTaskViews.User = tsk.User;
                NewTaskViews.AssignedUser = tsk.AssignedUser;
                NewTaskViews.File = tsk.File;
                TaskViews.Add(NewTaskViews);
            }
            return TaskViews;
        }

        public Model.Task GetTaskByID(int ID)
        {
            Model.Task Task = TaskRepository.GetByID(ID);
            return Task;
        }

        public void CreateTask(Model.Task Task)
        {

            Task.UserID = 1; //created by
            TaskRepository.Add(Task);
            SaveTask();
        }

        public void EditTask(Model.Task Task)
        {
            Task.User = null;
            Task.AssignedUser = null;
            Task.TaskStatus = null;

            Model.Task OldTask = TaskRepository.GetByID(Task.ID);
            if (OldTask.File != null && Task.File == null)
                Task.File = OldTask.File;

            TaskRepository.Edit(Task.ID, Task);
            SaveTask();
        }

        public void DeleteTask(int ID)
        {
            Model.Task Task = TaskRepository.GetByID(ID);
            TaskRepository.Delete(Task);
            SaveTask();
        }
        public void SaveTask()
        {
            unitOfWork.Commit();
        }

        public List<TaskView> CloseTask(int ID)
        {
            Model.Task Task = TaskRepository.GetByID(ID);
            Task.User = null;
            Task.AssignedUser = null;
            Task.TaskStatus = null;
            Task.TaskStatusID = TaskStatusRepository.Get(x => x.Status == "Closed").FirstOrDefault().ID;
            TaskRepository.Edit(Task.ID, Task);
            SaveTask();

            List<TaskView> AllTasks = GetTasks();
            return AllTasks;
        }


        public List<TaskView> AddComment(CommentView NewComment)
        {
            NewComment.CreationDate = DateTime.Now;

            Comment Comment = new Comment()
            {
                CommentDescription = NewComment.CommentDescription,
                UserID = 1, //current logged in user
                ParentCommentID = NewComment.ParentID,
                CreationDate = NewComment.CreationDate
            };
            CommentRepository.Add(Comment);
            SaveTask();
            List<TaskView> Tasks = GetTasks();
            return Tasks;
        }

    }
}
