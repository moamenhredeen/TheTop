using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.ViewModels;

namespace TheTop.Controllers
{
    public class CustomersController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public CustomersController( UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<ActionResult> GetAllCust()
        {
            var programmers = await _userManager.GetUsersInRoleAsync("Customer");

            List<CustomerVM> customersList = programmers.Select(customer => new CustomerVM
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                BirthDate = customer.BirthDate,
                Country = customer.Country,
                City = customer.City,
                Phone = customer.PhoneNumber,
                ID = customer.Id,
                Username = customer.UserName,
            }).ToList();

            
            return View(customersList);
        }

        public async Task<ActionResult> Details(string custId)
        {
            var customer = await _userManager.FindByIdAsync(custId);

            CustomerVM customerVM = new CustomerVM
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                BirthDate = customer.BirthDate,
                Country = customer.Country,
                City = customer.City,
                Phone = customer.PhoneNumber,
                ID = customer.Id,
                Username = customer.UserName,
            };
            return View(customerVM);
        }

        public async Task<ActionResult> profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var customer = await _userManager.FindByIdAsync(user.Id);
            CustomerVM customerVM = new CustomerVM
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                BirthDate = customer.BirthDate,
                Country = customer.Country,
                City = customer.City,
                Phone = customer.PhoneNumber,
                ID = customer.Id,
                Username = customer.UserName,
            };
            return View(customerVM);
        }
    }
}
