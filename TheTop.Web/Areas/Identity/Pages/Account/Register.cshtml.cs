using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TheTop.Application.Entities;
using TheTop.Application.Services;
using TheTop.Application.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace TheTop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(
            ILogger<RegisterModel> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [BindProperty] public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                // TODO : Delete
                if (!await _roleManager.RoleExistsAsync("Customer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                var user = new ApplicationUser {
                    UserName = Input.Username,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    BirthDate = Input.BirthDate,
                    PhoneNumber = Input.Phone,
                    ImagName = Input.ImageName,
                    City = Input.City,
                    Country = Input.Country,
                };
                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, "Customer");
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }


        // submitted form data 
        public class InputModel
        {

            [Display(Name = "First Name")]
            [MaxLength(55, ErrorMessage = "First Name shoald not exced 55 char!")]
            [MinLength(3, ErrorMessage = "First Name should not be less than 3 char!")]
            [Required(ErrorMessage = "First Name is required")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [MaxLength(55, ErrorMessage = "Last Name shoald not exced 55 char!")]
            [MinLength(3, ErrorMessage = "Last Name should not be less than 3 char!")]
            [Required(ErrorMessage = "Last Name is required")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Birth Date")]
            [Required(ErrorMessage = "Birth Date is required")]
            public DateTime BirthDate { get; set; }

            [StringLength(55, MinimumLength = 3, ErrorMessage = "Username should have 3 up to 55 char!")]
            [Required(ErrorMessage = "Username is required")]
            public string Username { get; set; }

            [Phone(ErrorMessage = "Invalid phone number")]
            [Display(Name = "Phone", Prompt = "+00 000 000 000")]
            [Required(ErrorMessage = "Phone is required")]
            public string Phone { get; set; }

            [StringLength(255, MinimumLength = 3, ErrorMessage = "Username should have 3 up to 255 char!")]
            [Required(ErrorMessage = "City is required")]
            public string City { get; set; }

            [StringLength(255, MinimumLength = 3, ErrorMessage = "Username should have 3 up to 255 char!")]
            [Required(ErrorMessage = "Country is required")]
            public string Country { get; set; }

            public string ImageName { get; set; }
        }
    }
}