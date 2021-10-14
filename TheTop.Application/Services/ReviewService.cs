using ApplicationModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class ReviewService
    {
        private AppDbContext _appDbContext;

        //Review Service

        public ReviewService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        public void CreateNewReview(ReviewDTO reviewDto)
         {
            _appDbContext.Add(new Review()
            {
                Subject = reviewDto.Subject,
                Massage = reviewDto.Massage,
                ApplicationUserId = reviewDto.UserId,
                Approved = false,
                CreatedAt = DateTime.Now,
                
            });
            _appDbContext.SaveChanges();
        }
        public void ApproveReview(int reviewId)
        {
            var review = _appDbContext.Reviews
                       .Where(review => review
                       .ReviewId == reviewId).Include(review => review.ApplicationUser).Single();

            review.Approved = !review.Approved ;
             
            _appDbContext.SaveChanges();

        }
        public void RemoveReview(int id)
        {
            _appDbContext.Remove(new Review { ReviewId = id });
            _appDbContext.SaveChanges();
        }

        public ReviewDTO GetReviewById(int reviewId)
        {
            var review = _appDbContext.Reviews
                        .Where(review => review
                        .ReviewId == reviewId).Include(review => review.ApplicationUser).Single();

            return new ReviewDTO { 
              ID = review.ReviewId,
              Massage = review.Massage,
              Subject = review.Subject,
              CreatedAt = review.CreatedAt,
              Approved = review.Approved,
              User = new UserDTO {
                  //ID = review.ApplicationUserId,
                  FirstName = review.ApplicationUser.FirstName,
                  LastName = review.ApplicationUser.LastName,
                  Email = review.ApplicationUser.Email,
                  //ImagName = review.ApplicationUser.ImagName,               
              }
            };
        }
        public IEnumerable<ReviewDTO> GetAllAllReviews()
        {
            var reviewsList = _appDbContext.Reviews.Include(review => review.ApplicationUser).ToList();

            return reviewsList.Select(review => new ReviewDTO
            {
                ID = review.ReviewId,
                Massage = review.Massage,
                Approved = review.Approved,
                Subject = review.Subject,
                CreatedAt = review.CreatedAt,
                User = new UserDTO
                {
                    FirstName = review.ApplicationUser.FirstName,
                    LastName = review.ApplicationUser.LastName,
                    Email = review.ApplicationUser.Email,
                    //ImagName = review.ApplicationUser.ImagName,
                }
            });        
        }
        public IEnumerable<ReviewDTO> GetApprovedReviews()
        {
            var reviewsList = _appDbContext.Reviews
                             .Where(review => review.
                             Approved == true).Include(review => review.ApplicationUser).ToList();

            return reviewsList.Select(review => new ReviewDTO
            {
                
                Massage = review.Massage,
                Approved = review.Approved,
                Subject = review.Subject,
                CreatedAt = review.CreatedAt,
                User = new UserDTO
                {
                    FirstName = review.ApplicationUser.FirstName,
                    LastName = review.ApplicationUser.LastName,
                    Email = review.ApplicationUser.Email,
                    //ImagName = review.ApplicationUser.ImagName,
                }
            });
        }

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

        // Contract Service
        public void CreateNewContract(ContractDTO contractDto)
        {
            _appDbContext.Add(new Contract {
             HourSalary = contractDto.HourSalary,
             MonthlyWorkingHours = contractDto.MonthlyWorkingHours,
              //UserID           
            });

            _appDbContext.SaveChanges();
        }
        public void UpdateContract(ContractDTO contractDto)
        {
            _appDbContext.Update(new Contract
            {
                UpdatedAt = DateTime.Now,
                HourSalary = contractDto.HourSalary,
                MonthlyWorkingHours = contractDto.MonthlyWorkingHours,
                //UserID           
            });

            _appDbContext.SaveChanges();
        }
        public IEnumerable<ContractDTO> GetAllContract()
        {
            var contractsList = _appDbContext.Contracts.AsNoTracking().ToList();

            return contractsList.Select(contract => new ContractDTO
            {
                HourSalary = contract.HourSalary,
                MonthlyWorkingHours = contract.MonthlyWorkingHours,
                CreateAt = contract.CreateAt
                //User
            });
        } 

        // Work Service

        public void StartWork(WorkDTO workDto)
        {
               
            if (FindStartDate(workDto.StartDate) == null)
            {
                _appDbContext.Add(new Work
                {
                    ApplicationUserId = workDto.ApplicationUserId,
                    StartDate = workDto.StartDate
                });
                _appDbContext.SaveChanges();
            }
          
        }
        public void EndWork(WorkDTO workDto)
        {
            var workStart = FindStartDate(workDto.EndDate);
            var workEnd = FindEndDate(workDto.EndDate);

            if (workStart != null && workEnd == null)
            {
                _appDbContext.Update(new Work
                {
                    WorkId = workStart.WorkId,
                    ApplicationUserId = workStart.ApplicationUserId,
                    StartDate = workStart.StartDate,
                    EndDate = workDto.EndDate
                }) ; 
                _appDbContext.SaveChanges();
            }

        }
        public Work FindStartDate(DateTime date)
        {
            var data = _appDbContext.Works.Where(w => w.StartDate.Date == date.Date).AsNoTracking().SingleOrDefault();

            return data;
        }
        public Work FindEndDate(DateTime date)
        {
            var data = _appDbContext.Works.Where(w => w.EndDate.Value.Date == date.Date).AsNoTracking().SingleOrDefault();

            return data;
        }
    }
}
