using System.Collections.Generic;
using System.Linq;
using ApplicationModel;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class TaskService : ITaskService
    {
        
        private readonly AppDbContext _appDbContext;
        public TaskService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        // Task Service
        public void CreateNewTask(TaskDTO taskTdo)
        {
            _appDbContext.Add(new TaskEntity
            {
               Title = taskTdo.Title,
               Description = taskTdo.Description,
               Duration = taskTdo.Duration,
               DueDate = taskTdo.DueDate,
               Status = taskTdo.Status == StatusType.Done.ToString() ?
                        StatusType.Done:
                        taskTdo.Status == StatusType.InProgress.ToString() ?
                        StatusType.InProgress : StatusType.Todo,
               Priority = taskTdo.Priority == PriorityType.High.ToString() ?
                        PriorityType.High :
                        taskTdo.Status == PriorityType.Low.ToString() ?
                        PriorityType.Low : PriorityType.Med,
                ApplicationUserId = taskTdo.ApplicationUserId
            });

            _appDbContext.SaveChanges();
        }
        public void UpdateTask(TaskDTO taskTdo)
        {
            _appDbContext.Update(new TaskEntity
            {
                TaskEntityId = taskTdo.ID,
                Title = taskTdo.Title,
                Description = taskTdo.Description,
                Duration = taskTdo.Duration,
                DueDate = taskTdo.DueDate,
                Status = taskTdo.Status == StatusType.Done.ToString() ?
                        StatusType.Done :
                        taskTdo.Status == StatusType.InProgress.ToString() ?
                        StatusType.InProgress : StatusType.Todo,
                Priority = taskTdo.Priority == PriorityType.High.ToString() ?
                        PriorityType.High :
                        taskTdo.Status == PriorityType.Low.ToString() ?
                        PriorityType.Low : PriorityType.Med,
                ApplicationUserId = taskTdo.ApplicationUserId
            });

            _appDbContext.SaveChanges();
        }
        public void RemoveTask(int taskId)
        {
            _appDbContext.Remove(new TaskEntity { TaskEntityId = taskId });
            _appDbContext.SaveChanges();
        }
        public TaskDTO GetTaskById(int taskId)
        {
            var task = _appDbContext.TaskEntities.Where(task => task.TaskEntityId == taskId)
                       .Include(task => task.ApplicationUser).Single();
                    

            return new TaskDTO {
                ID = task.TaskEntityId,
                Title = task.Title,
                Description = task.Description,
                Duration = task.Duration,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                User = new UserDTO
                {
                    FirstName = task.ApplicationUser.FirstName,
                    LastName = task.ApplicationUser.LastName,
                    Email = task.ApplicationUser.Email,
                    //ImagName = task.ApplicationUser.ImagName,
                    Country = task.ApplicationUser.Country,
                    Username = task.ApplicationUser.UserName
                }
            };
        }
        public IEnumerable<TaskDTO> GetEmployeeTasks(string employeeId)
        {
            var tasksList = _appDbContext.TaskEntities
                           .Where(task => task.ApplicationUserId == employeeId)
                           .ToList();

            return tasksList.Select(task => new TaskDTO { 
               ID = task.TaskEntityId,
               Title = task.Title,
               Description = task.Description,
               DueDate = task.DueDate,
               Duration = task.Duration,
               Priority  = task.Priority.ToString(),
               Status = task.Status.ToString(),
               CreatedAt = task.CreatedAt
            });
        }
        public IEnumerable<TaskDTO> GetAllTasks()
        {
            var tasksList = _appDbContext.TaskEntities.Include(task => task.ApplicationUser).ToList();

            return tasksList.Select(task => new TaskDTO
            {
                ID = task.TaskEntityId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Duration = task.Duration,
                Priority = task.Priority.ToString(),
                Status = task.Status.ToString(),
                CreatedAt = task.CreatedAt,
                User = new UserDTO
                {
                    FirstName = task.ApplicationUser.FirstName,
                    LastName = task.ApplicationUser.LastName,
                    Email = task.ApplicationUser.Email,
                    ImagName = task.ApplicationUser.ImagName,
                    Country = task.ApplicationUser.Country,
                    Username = task.ApplicationUser.UserName
                }
            });
        }

    }
}