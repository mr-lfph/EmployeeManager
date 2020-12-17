using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagerRazorPages.Models;
using EmployeeManagerRazorPages.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagerRazorPages.Pages.EmployeeManager
{
    [Authorize(Roles ="Manager")]
    public class ListModel : PageModel
    {
        private readonly AppDbContext db = null;
        public List<Employee> Employees { get; set; }
        public ListModel(AppDbContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            Employees = (from e in db.Employees orderby e.EmployeeID select e).ToList();
        }
    }
}
