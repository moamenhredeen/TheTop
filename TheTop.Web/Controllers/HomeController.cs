using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using TheTop.Models;

namespace TheTop.Controllers
{
    
    // [Authorize] // -> authenticated 
    /*[Authorize(Roles="Admin")]*/ // -> authenticated 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdvertisementService _service;
        public HomeController(ILogger<HomeController> logger, AdvertisementService service)
        {
            _logger = logger;
            _service = service;
        }
        List<AdvertisementVM> list = new List<AdvertisementVM>()
            {
                new AdvertisementVM{Name="Car1",Price=70,CategoryId = 1,
                   },
                new AdvertisementVM{Name="Car1",Price=60,CategoryId = 2,
                   },
                new AdvertisementVM{Name="Car2",Price=30,CategoryId = 3,
                    },
                 new AdvertisementVM{Name="Car2",Price=50,CategoryId = 2,
                   },
                new AdvertisementVM{Name="Car3",Price=40,CategoryId = 3,
                  }
            };

        public IActionResult HomePage()
        {
            ViewBag.listC = new List<string> { "kenan11", "kenan22", "kenan33" };
            List<CategoryDTO> categoryList = _service.GetAllCategories().ToList();
           // ViewBag.listC = categoryList.Select(categ => categ.Name);


            List<AdvertisementDTO> advertisementsDtoList = _service.GetAllAdvertisemensts().ToList();
            List<AdvertisementVM> advertisementsVMList = advertisementsDtoList
                                  .Select(advertisement => new AdvertisementVM
                                  {
                                      ID = advertisement.ID,
                                      Name = advertisement.Name,
                                      Price = advertisement.Price,
                                      Category = advertisement.CategoryName,
                                      CreatedAT = advertisement.CreatedAt,
                                      PhotosNames = advertisement.ImagesNames.Select(img => img).ToList(),
                                  }).ToList();

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
               AdvertisementsList = advertisementsVMList,
                ReviewsList = listRev,
                Review = new ReviewDTO(),               
            };
            ViewBag.numCart = 5;
            return View(model);
        }

        public IActionResult Cart(string id)
        {
           
            return View(new AdvertisementVM
            {
                Name = "Car1",
                Price = 100,
                CategoryId = 1,
              
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
