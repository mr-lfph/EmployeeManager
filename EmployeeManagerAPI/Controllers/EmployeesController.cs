using EmployeeManagerAPI.Models;
using EmployeeManagerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerAPI.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository employeeRepository = null;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet]
        public List<Employee> Get()
        {
            return employeeRepository.SellectAll();
        }
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeRepository.SelectById(id);
        }
        [HttpPost]
        public void Post([FromBody] Employee emp)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Insert(emp);
            }
        }
        [HttpPut("{id}")]
        public void Put( int id,[FromBody] Employee emp)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Update(emp);
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeRepository.Delete(id);
        }
    }
}
