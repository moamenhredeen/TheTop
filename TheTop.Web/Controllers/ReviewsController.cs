using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class ReviewsController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ReviewService _service;

        public ReviewsController(UserManager<ApplicationUser> userManager, ReviewService service)
        {
            this._userManager = userManager;
            this._service = service;
        }
        // GET: ReviewDTOsController
        public ActionResult Reviews()
        {
            List<ReviewDTO> reviewDtoList = _service.GetAllAllReviews().ToList();

            List<ReviewVM> reviewVMlist = new List<ReviewVM>();
            reviewDtoList.ForEach(review =>
            {
                reviewVMlist.Add(new ReviewVM { 
                 Customer = new CustomerVM
                 {
                     FirstName = review.User.FirstName,
                     LastName = review.User.LastName,
                     Email = review.User.Email,
                     //ImageName = review.User.ImagName
                 },
                 Massage = review.Massage,
                 Subject = review.Subject,
                 ID = review.ID,
                 Approved = review.Approved
                });
            });
            return View(reviewVMlist);
        }

        //GET: ReviewDTOsController/Details/5
        public ActionResult Details(int id)
        {
            ReviewDTO reviewDTO = _service.GetReviewById(id);
            return View(new ReviewVM {
                Customer = new CustomerVM
                {
                    FirstName = reviewDTO.User.FirstName,
                    LastName = reviewDTO.User.LastName,
                    Email = reviewDTO.User.Email,
                    //ImageName = review.User.ImagName
                },
                ID = reviewDTO.ID,
                Massage = reviewDTO.Massage,
              Subject = reviewDTO.Subject,
              Approved = reviewDTO.Approved,
            });
        }

        // GET: ReviewDTOsController/Create
        public ActionResult Create()
        {
            return View(new ReviewVM());
        }

        // POST: ReviewDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReviewVM reviewVM)
        {
            var user = await _userManager.GetUserAsync(User);
            if (! ModelState.IsValid)
            {               
                return RedirectToAction("HomePage", "Home");
            }
            _service.CreateNewReview(new ReviewDTO
            {
                
                Massage = reviewVM.Massage,
                Subject = reviewVM.Subject,
                UserId = user.Id,
            });
            return RedirectToAction("HomePage", "Home");
        }

        // GET: ReviewDTOsController/Edit/5
        public ActionResult ApproveReview(int id)
        {
            _service.ApproveReview(id);
            return RedirectToAction("Reviews");
        }


        public ActionResult DeleteReview(int id)
        {
            _service.RemoveReview(id);
            return RedirectToAction("Reviews");
        }

    }
}
