using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITaskService
    {
        List<Model.Task> GetTasks();
        Model.Task GetTaskByID(int ID);
        void CreateTask(Model.Task Task);
        void EditTask(Model.Task Task);
        void DeleteTask(int ID);
        void SaveTask();
    }
}
