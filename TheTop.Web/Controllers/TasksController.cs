using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class TasksController : Controller
    {
       static List<TaskDTO> list = new List<TaskDTO>()
            {
                new TaskDTO{Title = "Task1", Description ="kenna hassan rawashdeh ",
                    Priority =PriorityType.Low,Status=StatusType.Done,Duration=5,
                    DueDate= DateTime.Now,EmployeeId = 1},
                 new TaskDTO{Title = "Task2", Description ="dsdf dsfdsf kenna hassan rawashdeh ",
                    Priority =PriorityType.High,Status=StatusType.InProgress,Duration=10,
                    DueDate= DateTime.Now,EmployeeId = 2},
                  new TaskDTO{Title = "Task3", Description ="dfdsf dsfdsf rawdsashdeh ",
                    Priority =PriorityType.Med,Status=StatusType.Todo,Duration=3,
                    DueDate= DateTime.Now,EmployeeId = 3},
                  new TaskDTO {
                Title = "Task4",
                Description = "dfdsf dsfdsf rawdsashdeh ",
                Priority = PriorityType.Med,
                Status = StatusType.Todo,
                Duration = 7,
                DueDate = DateTime.Now,
                EmployeeId = 4
            }
            };
        // GET: TaskDTOsController
        public ActionResult Index()
        {
           
            return View(list);
        }

        public ActionResult GetTasksProg(int id)
        {
           
            return View(list);
        }

        // GET: TaskDTOsController/Details/5
        public ActionResult Details(int id)
        {
            TaskDTO obj = new TaskDTO
            {
                Title = "Task4",
                Description = "dfdsf dsfdsf rawdsashdeh ",
                Priority = PriorityType.Med,
                Status = StatusType.Todo,
                Duration = 7,
                DueDate = DateTime.Now,
                EmployeeId = 4
            };
            return View(obj);
        }

        // GET: TaskDTOsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskDTOsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {


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

        // GET: TaskDTOsController/Edit/5
        public ActionResult Edit(int id)
        {

            TaskDTO obj = new TaskDTO
            {
                Title = "Task4",
                Description = "dfdsf dsfdsf rawdsashdeh ",
                Priority = PriorityType.Med,
                Status = StatusType.Todo,
                Duration = 7,
                DueDate = DateTime.Now,
                EmployeeId = 4
            };
            return View(obj);
        }

        // POST: TaskDTOsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TaskDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {


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

        // GET: TaskDTOsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskDTOsController/Delete/5
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
