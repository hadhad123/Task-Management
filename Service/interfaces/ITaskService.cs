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
        List<TaskViewModel> GetTasks();
        TaskViewModel GetTaskByID(int ID);
        void CreateTask(Model.Task Task);
        void EditTask(Model.Task Task);
        void DeleteTask(int ID);
        void SaveTask();
        List<TaskViewModel> CloseTask(int ID);
        List<TaskViewModel> AddComment(CommentView NewComment);
    }
}
