using EmployeeManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerAPI.Repositories
{
    public interface IEmployeeRepository//call it also in startup.cs
    {
        List<Employee> SellectAll();
        Employee SelectById(int id);
        void Insert(Employee emp);
        void Update(Employee emp);
        void Delete(int id);

    }
}
