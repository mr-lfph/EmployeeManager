using EmployeeManagerBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerBlazor.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext db = null;

        public EmployeeRepository(AppDbContext _db)
        {
            db = _db;
        }
        public void Delete(int id)
        {
            Employee emp = db.Employees.Find(id);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }

        public void Insert(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
        }

        public List<Employee> SelectAll()
        {
            return db.Employees.ToList();
        }

        public Employee SelectByID(int id)
        {
            return db.Employees.Find(id);
        }

        public List<string> SelectCountries()
        {
            return db.Employees.Select(c => c.Country).Distinct().ToList();
        }

        public void Update(Employee emp)
        {
            db.Employees.Update(emp);
            db.SaveChanges();
        }
    }
}
