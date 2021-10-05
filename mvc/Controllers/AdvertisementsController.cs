using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Models;

namespace TheTop.Controllers
{
    public class AdvertisementsController : Controller
    {

       static List<AdvertisementDTO> list = new List<AdvertisementDTO>()
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
        // GET: AdvertisementDTOsController
        public ActionResult Index()
        {
            ViewBag.listC = new List<string> {"Car1","Car2","Car3"};
            ViewBag.listRev = new List<ReviewDTO>()
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

        public ActionResult GetById(int id)
        {

            return View(list);
        }

        public ActionResult Search()
        {
            SearchDTO model = new SearchDTO();
            model.Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
            };
           // ViewBag.listC = new List<string> { "Car1", "Car2", "Car3" };

            model.Advertisements =  new List<AdvertisementDTO>()
            {
                new AdvertisementDTO{Name="Car1",Price=70,CategoryId = 1,
                Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
                 }, createDate = DateTime.Now },
                new AdvertisementDTO{Name="Car1",Price=60,CategoryId = 2,
                  Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
                 },  createDate = DateTime.Now },
                new AdvertisementDTO{Name="Car2",Price=30,CategoryId = 3,
                    Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
                 },  createDate = DateTime.Now },
                 new AdvertisementDTO{Name="Car2",Price=50,CategoryId = 2,
                    Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
                 },  createDate = DateTime.Now },
                new AdvertisementDTO{Name="Car3",Price=40,CategoryId = 3,
                    Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
                 },  createDate = DateTime.Now }
            };

            //model.Categorys = new SelectList(list,"ID","Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchDTO model)
        {
            Console.WriteLine(model);
            return View(model);
        }

        // GET: AdvertisementDTOsController/Details/5
        public ActionResult Details(string name)
        {
            var obj = list.FirstOrDefault(e => e.Name == name);
            return View(obj);
        }

        // GET: AdvertisementDTOsController/Create
        public ActionResult Create()
        {

            AdvertisementDTO model = new AdvertisementDTO();
             model.Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
            };
            return View(model);
        }

        // POST: AdvertisementDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdvertisementDTO model)
        {
            var uploads = "C:\\ImageTheTop";
            try
            {
                if (ModelState.IsValid)
                {
                    var imagesNames = new List<string>();
                    foreach(var file in model.PhotosNames)
                    {
                        if (file.Length > 0)
                        {
                            string filePath = Path.Combine(uploads, file.FileName);
                            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            imagesNames.Add(file.FileName);
                        }

                    }

                    list.Add(model);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertisementDTOsController/Edit/5
        public ActionResult Edit(string name)
        {
            var obj = list.FirstOrDefault(e => e.Name == name);
            obj.Categorys = new List<CategoryDTO>{
                new CategoryDTO{ID = 1, Name = "Car1"},
                new CategoryDTO{ID = 2, Name = "Car2"},
                new CategoryDTO{ID = 3, Name = "Car3"},
            };
            return View(obj);
        }

        // POST: AdvertisementDTOsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AdvertisementDTO model)
        {
            try
            {
                Console.WriteLine(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdvertisementDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdvertisementDTOsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
