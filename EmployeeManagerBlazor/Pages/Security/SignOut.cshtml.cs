using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagerBlazor.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagerBlazor.Pages.Security
{
    [Authorize()]
    public class SignOutModel : PageModel
    {

        private readonly SignInManager<AppIdentityUser> signinManager;
        public SignOutModel(SignInManager<AppIdentityUser> signInManager)
        {
            signinManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await signinManager.SignOutAsync();
            return RedirectToPage("/Security/SignIn");
        }
    }
}
