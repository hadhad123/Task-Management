using Model;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITaskService
    {
        List<TaskView> GetTasks();
        Model.Task GetTaskByID(int ID);
        void CreateTask(Model.Task Task);
        void EditTask(Model.Task Task);
        void DeleteTask(int ID);
        void SaveTask();
        List<TaskView> CloseTask(int ID);
        List<TaskView> AddComment(CommentView NewComment);
    }
}
