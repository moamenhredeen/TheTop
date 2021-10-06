using ApplicationModel1.Dao;
using ApplicationModel1.Entities;
using ApplicationModel1.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Services
{
    class ReviewService
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
               Status = taskTdo.Status,
               Priority = taskTdo.Priority,
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
                Status = taskTdo.Status,
                Priority = taskTdo.Priority,
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
            var task = _appDbContext.TaskEntities
                      .SingleOrDefault(task => task.TaskEntityId == taskId);

            return new TaskDTO {
                ID = task.TaskEntityId,
                Title = task.Title,
                Description = task.Description,
                Duration = task.Duration,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority,
                User = new UserDTO
                {
                    FirstName = task.ApplicationUser.FirstName,
                    LastName = task.ApplicationUser.LastName,
                    Email = task.ApplicationUser.Email,
                    ImagName = task.ApplicationUser.ImagName,
                    Country = task.ApplicationUser.Country,
                    Username = task.ApplicationUser.UserName
                }
            };
        }
    }
}
