using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagerBlazor.Models;
using EmployeeManagerBlazor.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagerBlazor.Pages.Security
{
    public class SignInModel : PageModel
    {
        private readonly SignInManager<AppIdentityUser> signInManager;
        [BindProperty]
        public SignIn SignInData { get; set; }

        public SignInModel(SignInManager<AppIdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {// user pass for razor pages mizan1/Rp@2-7 
                var result = await signInManager.PasswordSignInAsync(
                    SignInData.UserName, SignInData.Password, SignInData.RememberMe, false);

                if (result.Succeeded)
                    return Redirect("/Employees/List");
                else
                    ModelState.AddModelError("", "Invalid username or password");

            }
            return Page();
        }
    }
}
