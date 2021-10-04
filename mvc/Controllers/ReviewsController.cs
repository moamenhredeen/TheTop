using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Models;

namespace TheTop.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: ReviewDTOsController
        public ActionResult Index()
        {
            List<ReviewDTO> list = new List<ReviewDTO>()
            {
                new ReviewDTO{Name="kenan",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Noor",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Moamen",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },
                new ReviewDTO{Name="Wael",Email="kenan@gmail.com",
                              Subject ="Erorr",Massage="Sed tamen tempor magna labore dolore dolor" +
                              " sint tempor duis magna elit veniam aliqua esse amet veniam enim" },

            };
            return View(list);
        }

        // GET: ReviewDTOsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ReviewDTOsController/Create
        public ActionResult Create()
        {
            return View(new ReviewDTO());
        }

        // POST: ReviewDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewDTO model)
        {
              model.ID = 1;
            if (ModelState.IsValid)
            { 
                
                Console.WriteLine(model);
                return RedirectToAction("HomePage", "Home");
            }
            return RedirectToAction("HomePage", "Home");
        }

        // GET: ReviewDTOsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ReviewDTOsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ReviewDTOsController/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: ReviewDTOsController/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
    }
}
