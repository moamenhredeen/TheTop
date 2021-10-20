using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface IReviewService
    {
        void CreateNewReview(ReviewDTO reviewDto);
        void ApproveReview(int reviewId);
        void RemoveReview(int id);
        ReviewDTO GetReviewById(int reviewId);
        IEnumerable<ReviewDTO> GetAllAllReviews();
        IEnumerable<ReviewDTO> GetApprovedReviews();
    }
}