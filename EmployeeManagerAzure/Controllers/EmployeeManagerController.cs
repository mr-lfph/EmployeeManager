using EmployeeManagerAzure.Models;
using EmployeeManagerAzure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerAzure.Controllers
{
    public class EmployeeManagerController : Controller
    {
        private IEmployeeRepository employeeRepository;
        public EmployeeManagerController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public IActionResult List()
        {
            List<Employee> model = employeeRepository.SelectAll();
            return View(model);
        }

        private void FillCoountries()
        {
            List<string> countriesList = employeeRepository.SelectCountries();
            List<SelectListItem> countries = (from c in countriesList
                                              orderby c ascending
                                              select new SelectListItem()
                                              {
                                                  Text = c,
                                                  Value = c
                                              }).ToList();
            ViewBag.Countries = countries;
        }

        [HttpGet]
        public IActionResult Insert()
        {
            FillCoountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Employee model)
        {
            FillCoountries();
            if (ModelState.IsValid)
            {
                employeeRepository.Insert(model);
                ViewBag.Message = "Employee Inserted Successfully";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            FillCoountries();
            Employee model = employeeRepository.SelectByID(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Employee model)
        {
            FillCoountries();
            if (ModelState.IsValid)
            {
                employeeRepository.Update(model);
                ViewBag.Message = "Employee Updated Successfully";
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            Employee model = employeeRepository.SelectByID(id);
            return View(model);   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            employeeRepository.Delete(id);
            TempData["Message"] = "Employee Deleted Successfully";
            return RedirectToAction("List");
        }









    }
}
