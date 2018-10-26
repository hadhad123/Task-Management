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
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository TaskRepository;
        private readonly IUnitOfWork unitOfWork;

        public TaskService(ITaskRepository TaskRepository,IUnitOfWork unitOfWork)
        {
            this.TaskRepository = TaskRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Model.Task> GetTasks()
        {
            List<string> Includes = new List<string>();
            Includes.Add("User");
            Includes.Add("AssignedUser");
            Includes.Add("TaskStatus");
            Includes.Add("Comments");

            List<Model.Task> Tasks = TaskRepository.GetAllWithIncludes(Includes).ToList();
            return Tasks;
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


    }
}
