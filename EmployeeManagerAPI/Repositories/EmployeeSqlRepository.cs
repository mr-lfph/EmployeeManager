using EmployeeManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerAPI.Repositories
{
    public class EmployeeSqlRepository : IEmployeeRepository
    {
        private readonly AppDbContext db = null;
        public EmployeeSqlRepository(AppDbContext _db)
        {
            db = _db;
        }
        public void Delete(int id)
        {
            db.Database.ExecuteSqlRaw("delete from Employees where EmployeeId={0}", id);
        }

        public void Insert(Employee emp)
        {
            db.Database.ExecuteSqlRaw("insert into Employees (FirstName, LastName,Title,BirthDate,HireDate,Country,Notes ) " +
                "values({0},{1},{2},{3},{4},{5},{6})", emp. FirstName, emp.LastName, emp.Title, emp.BirthDate, emp.HireDate, emp.Country, emp.Notes);
        }
        public void Update(Employee emp)
        {
            db.Database.ExecuteSqlRaw(@"update Employees set FirstName={0}, LastName={1},Title={2},BirthDate={3},HireDate={4},Country={5},Notes={6} 
                where employeeId={7}",emp. FirstName, emp.LastName, emp.Title, emp.BirthDate, emp.HireDate, emp.Country, emp.Notes,emp.EmployeeID);
        }

        public Employee SelectById(int id)
        {
            Employee emp = db.Employees.FromSqlRaw("select EmployeeId,FirstName, LastName,Title,BirthDate,HireDate,Country,Notes from Employees where EmployeeId ={0}", id).SingleOrDefault();
            return emp;
        }

        public List<Employee> SellectAll()
        {
            List<Employee> data = db.Employees.FromSqlRaw(@"select EmployeeId,FirstName, LastName,Title,BirthDate,HireDate,Country,Notes from Employees 
               order by  EmployeeId ").ToList();
            return data;
        }

       
    }
}
