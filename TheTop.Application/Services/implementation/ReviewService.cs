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
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _appDbContext;
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




    }
}
