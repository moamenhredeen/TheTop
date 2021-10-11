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

        //Review Service
        private AppDbContext _appDbContext;
        public ReviewService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        public void CreateNewReview(ReviewsDTO reviewDto)
         {
            _appDbContext.Add(new Review()
            {
                Name = reviewDto.Name,
                Subject = reviewDto.Subject,
                Email = reviewDto.Email,
                Massage = reviewDto.Massage,
                ApplicationUserId = reviewDto.UserId,
                Approved = false,
            });
            _appDbContext.SaveChanges();
        }
        public void UpdateReview(ReviewsDTO reviewDto)
        {
            Review reviewModel = new Review()
            {
                ReviewId = reviewDto.ID,
                Name = reviewDto.Name,
                Subject = reviewDto.Subject,
                Email = reviewDto.Email,
                Massage = reviewDto.Massage,
                ApplicationUserId = reviewDto.UserId,
                UpdatedAt = DateTime.Now
            };

            reviewModel.Approved = reviewDto.Approved == false ? true : false;
             
          
            _appDbContext.Update(reviewModel);
            _appDbContext.SaveChanges();

        }
        public void RemoveReview(int id)
        {
            _appDbContext.Remove(new Review { ReviewId = id });
            _appDbContext.SaveChanges();
        }
        public ReviewsDTO GetReviewById(int reviewId)
        {
            var review = _appDbContext.Reviews
                        .SingleOrDefault(review => review
                        .ReviewId == reviewId);

            return new ReviewsDTO { 
              ID = review.ReviewId,
              Name = review.Name,
              Email = review.Email,
              Massage = review.Massage,
              Subject = review.Subject,
              CreatedAt = review.CreatedAt,
              User = new UserDTO {
                  ID = review.ApplicationUserId, 
                  FirstName = review.ApplicationUser.FirstName,
                  LastName = review.ApplicationUser.LastName,
                  Email = review.ApplicationUser.Email,
                  ImagName = review.ApplicationUser.ImagName,
                  Country = review.ApplicationUser.Country,
                  Username = review.ApplicationUser.UserName,
              } 
            };
        }
        public IEnumerable<ReviewsDTO> GetAllAllReviews()
        {
            var reviewsList = _appDbContext.Reviews.AsNoTracking().ToList();

            return reviewsList.Select(review => new ReviewsDTO
            {
                Name = review.Name,
                Email = review.Email,
                Massage = review.Massage,
                Approved = review.Approved,
                Subject = review.Subject,
                CreatedAt = review.CreatedAt,
                User = new UserDTO
                {
                    FirstName = review.ApplicationUser.FirstName,
                    LastName = review.ApplicationUser.LastName,
                    Email = review.ApplicationUser.Email,
                    ImagName = review.ApplicationUser.ImagName,
                    Country = review.ApplicationUser.Country,                    
                }
            });        
        }
        public IEnumerable<ReviewsDTO> GetApprovedReviews()
        {
            var reviewsList = _appDbContext.Reviews
                             .Where(review => review.
                             Approved == true).ToList();

            return reviewsList.Select(review => new ReviewsDTO
            {
                Name = review.Name,
                Email = review.Email,
                Massage = review.Massage,
                Approved = review.Approved,
                Subject = review.Subject,
                CreatedAt = review.CreatedAt,
                User = new UserDTO
                {
                    FirstName = review.ApplicationUser.FirstName,
                    LastName = review.ApplicationUser.LastName,
                    Email = review.ApplicationUser.Email,
                    ImagName = review.ApplicationUser.ImagName,
                    Country = review.ApplicationUser.Country,
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
