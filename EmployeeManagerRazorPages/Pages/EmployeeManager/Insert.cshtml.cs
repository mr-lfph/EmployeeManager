using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagerRazorPages.Models;
using EmployeeManagerRazorPages.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagerRazorPages.Pages.EmployeeManager
{
    [Authorize(Roles = "Manager")]
    public class InsertModel : PageModel
    {
        private readonly AppDbContext db = null;

        [BindProperty]
        public Employee Employee { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public string Message { get; set; }

        public void FillCountries()
        {
            List<SelectListItem> listOfCountries = (from c in db.Employees
                                                    select new SelectListItem()
                                                    {
                                                        Text = c.Country,
                                                        Value = c.Country
                                                    }).Distinct().ToList();

            Countries = listOfCountries;
        }
        public InsertModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            FillCountries();
        }
        public void OnPost()
        {
            FillCountries();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Employees.Add(Employee);
                    db.SaveChanges();
                    Message = "Employee Inserted Successfully";
                }
                catch (DbUpdateException ex)
                {

                    Message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        Message += ex.InnerException.Message;
                    }
                }
                catch (Exception exOther)
                {
                    Message = exOther.Message;
                }
            }
        }
    }
}
