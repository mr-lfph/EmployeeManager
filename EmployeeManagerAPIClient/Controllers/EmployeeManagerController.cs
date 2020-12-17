using EmployeeManagerAPIClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagerAPIClient.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeeManagerController : Controller
    {
        private readonly HttpClient client = null;

        private string employeesApiUrl = "";

        public EmployeeManagerController(HttpClient client, IConfiguration config)
        {
            this.client = client;
            employeesApiUrl = config.GetValue<string>("AppSettings:EmployeesApiUrl");
        }

        public async Task<bool> FillCountriesAsync()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var countries = JsonSerializer.Deserialize<List<Employee>>(stringData, options).Select(c => c.Country).Distinct();

            List<SelectListItem> listOfCountries = new SelectList(countries).ToList();
            ViewBag.Countries = listOfCountries;
            return true;
        }

        public async Task<IActionResult> ListAsync()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Employee> data = JsonSerializer.Deserialize<List<Employee>>(stringData, options);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> InsertAsync()
        {
            await FillCountriesAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertAsync(Employee model)
        {
            await FillCountriesAsync();
            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(employeesApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Employee Inserted Successfully";
                else
                    ViewBag.Message = "Error when calling web api";
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            await FillCountriesAsync();
            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(Employee model)
        {

            await FillCountriesAsync();
            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{employeesApiUrl}/{model.EmployeeID}", contentData);
                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Employee updated Successfully";
                else
                    ViewBag.Message = "Error Updating when calling web api";
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int employeeId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{employeesApiUrl}/{employeeId}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Employee Deleted Syccessfully";
            }
            else
            {
                TempData["Message"] = "Error while calling web api for delation";
            }

            return RedirectToAction("List");
        }
    }
}
