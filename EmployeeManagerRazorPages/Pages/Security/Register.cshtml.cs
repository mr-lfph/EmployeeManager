using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagerRazorPages.Models;
using EmployeeManagerRazorPages.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagerRazorPages.Pages.Security
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;

        [BindProperty]
        public Register RegisterData { get; set; }

        public RegisterModel( UserManager<AppIdentityUser> userManager, RoleManager<AppIdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!await roleManager.RoleExistsAsync("Manager"))
                {
                    AppIdentityRole role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can Perform CRUD Operation";
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
                var user = new AppIdentityUser();
                user.UserName = RegisterData.UserName;
                user.Email = RegisterData.Email;
                user.FullName = RegisterData.FullName;
                user.BirthDate = RegisterData.BirthDate;

                var result = await userManager.CreateAsync(user, RegisterData.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                  return  RedirectToPage("/Security/SignIn");
                }
                else
                    ModelState.AddModelError("", "Invalid User Data");
            }
            return Page();
        }
    }
}
