using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TheTop.Models;

namespace TheTop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        List<AdvertisementDTO> list = new List<AdvertisementDTO>()
            {
                new AdvertisementDTO{Name="Car1",Price=70,CategoryId = 1,
                  createDate = DateTime.Now },
                new AdvertisementDTO{Name="Car1",Price=60,CategoryId = 2,
                   createDate = DateTime.Now },
                new AdvertisementDTO{Name="Car2",Price=30,CategoryId = 3,
                   createDate = DateTime.Now },
                 new AdvertisementDTO{Name="Car2",Price=50,CategoryId = 2,
                  createDate = DateTime.Now },
                new AdvertisementDTO{Name="Car3",Price=40,CategoryId = 3,
                  createDate = DateTime.Now }
            };

        public IActionResult HomePage(string id)
        {
            ViewBag.listC = new List<string> { "Car1", "Car2", "Car3" };
            List<ReviewDTO> listRev = new List<ReviewDTO>()
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
            HomeDTO model = new HomeDTO
            {
               AdvertisementsList = list,ReviewsList = listRev,Review = new ReviewDTO(),
               
            };
            ViewBag.numCart = 5;
            return View(model);
        }

        public IActionResult Cart(string id)
        {
           
            return View(new AdvertisementDTO
            {
                Name = "Car1",
                Price = 100,
                CategoryId = 1,
                createDate = DateTime.Now
            });
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
