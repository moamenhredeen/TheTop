using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface ITaskService
    {
        void CreateNewTask(TaskDTO taskTdo);
        void UpdateTask(TaskDTO taskTdo);
        void RemoveTask(int taskId);
        TaskDTO GetTaskById(int taskId);
        IEnumerable<TaskDTO> GetEmployeeTasks(string employeeId);
        IEnumerable<TaskDTO> GetAllTasks();
    }
}