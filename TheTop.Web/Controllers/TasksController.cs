using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class TasksController : Controller
    {
       static List<TaskVM> list = new List<TaskVM>()
            {
                new TaskVM{Title = "Task1", Description ="kenna hassan rawashdeh ",
                    Priority =PriorityType.Low,Status=StatusType.Done,Duration=5,
                    DueDate= DateTime.Now,EmployeeId = "1"},
                 new TaskVM{Title = "Task2", Description ="dsdf dsfdsf kenna hassan rawashdeh ",
                    Priority =PriorityType.High,Status=StatusType.InProgress,Duration=10,
                    DueDate= DateTime.Now,EmployeeId = "2"},
                  new TaskVM{Title = "Task3", Description ="dfdsf dsfdsf rawdsashdeh ",
                    Priority =PriorityType.Med,Status=StatusType.Todo,Duration=3,
                    DueDate= DateTime.Now,EmployeeId = "2"},
                  new TaskVM {
                Title = "Task4",
                Description = "dfdsf dsfdsf rawdsashdeh ",
                Priority = PriorityType.Med,
                Status = StatusType.Todo,
                Duration = 7,
                DueDate = DateTime.Now,
                EmployeeId = "4"
            }
            };

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ReviewService _service;
        public TasksController(UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               ReviewService service)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._service = service;
        }

        public ActionResult Index()
        {
            List<TaskDTO> taskDtoList = _service.GetAllTasks().ToList();
            List<TaskVM> taskVMList = new();
            taskDtoList.ForEach(task =>
            {
                taskVMList.Add(new TaskVM { 
                  Title = task.Title,
                  Description =task.Description,
                  DueDate = task.DueDate,
                  Duration = task.Duration,
                  ID = task.ID,
                   Priority = task.Priority == PriorityType.High.ToString() ?
                              PriorityType.High : task.Priority == PriorityType.Low.ToString() ?
                              PriorityType.Low : PriorityType.Med,
                   Status = task.Status == StatusType.Done.ToString() ?
                            StatusType.Done : task.Status == StatusType.InProgress.ToString() ?
                            StatusType.InProgress : StatusType.Todo,
                   EmployeeDTO = new EmployeeDTO
                    {
                        FirstName = task.User.FirstName,
                        LastName = task.User.LastName,
                        Email = task.User.Email,
                        Username = task.User.Username,
                        //ImageName = task.User.ImagName
                    }
                });
            });
            return View(taskVMList);
        }

        public ActionResult GetTasksProg(int id)
        {
           
            return View(list);
        }

        
        public ActionResult Details(int id)
        {
            TaskVM obj = new TaskVM
            {
                Title = "Task4",
                Description = "dfdsf dsfdsf rawdsashdeh ",
                Priority = PriorityType.Med,
                Status = StatusType.Todo,
                Duration = 7,
                DueDate = DateTime.Now,
                EmployeeId = "4"
            };
            return View(obj);
        }

        
        public async Task<ActionResult> Create()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Programmer");
            List<SelectListItem> employeeList = new();
            foreach(var e in employees)
            {
                employeeList.Add(new SelectListItem { Text = e.UserName, Value = e.Id });
            }
            TaskVM taskVM = new TaskVM();
            taskVM.Employees = employeeList;
            return View(taskVM);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskVM taskVM)
        {
                if (! ModelState.IsValid)
                {
                return View(taskVM);
                }
            _service.CreateNewTask(new TaskDTO { 
               Title = taskVM.Title,
               Description = taskVM.Description,
               ApplicationUserId = taskVM.EmployeeId,
               Duration = taskVM.Duration,
               DueDate = taskVM.DueDate,
               Priority = taskVM.Priority.ToString(),
               Status = taskVM.Status.ToString(),            
            });
            return RedirectToAction("HomePage", "Home");
        }


        public ActionResult Edit(int id)
        {

            TaskVM obj = new TaskVM
            {
                Title = "Task4",
                Description = "dfdsf dsfdsf rawdsashdeh ",
                Priority = PriorityType.Med,
                Status = StatusType.Todo,
                Duration = 7,
                DueDate = DateTime.Now,
                EmployeeId = "4"
            };
            return View(obj);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TaskVM model)
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

        
        public ActionResult Delete(int id)
        {
            return View();
        }

       
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
