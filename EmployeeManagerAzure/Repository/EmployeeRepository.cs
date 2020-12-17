using EmployeeManagerAzure.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerAzure.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private string connectionString;

        public EmployeeRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("AppDb");
        }

        public void Delete(int id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from  Employees where EmployeeID=@EmployeeID";
                SqlParameter p = new SqlParameter(@"EmployeeID", id);
                cmd.Parameters.Add(p);
                cnn.Open();

                int i = cmd.ExecuteNonQuery();

                cnn.Close();


            }
        }

        public void Insert(Employee emp)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employees(FirstName,LastName,Title,BirthDate,HireDate,Country,Notes)" +
                    " values(@FirstName,@LastName,@Title,@BirthDate,@HireDate,@Country,@Notes)";
               
                SqlParameter[] p = new SqlParameter[7];
                p[0] = new SqlParameter("@FirstName", emp.FirstName);
                p[1] = new SqlParameter("@LastName", emp.LastName);
                p[2] = new SqlParameter("@Title", emp.Title);
                p[3] = new SqlParameter("@BirthDate", emp.BirthDate);
                p[4] = new SqlParameter("@HireDate", emp.HireDate);
                p[5] = new SqlParameter("@Country", emp.Country);
                p[6] = new SqlParameter("@Notes", emp.Notes ?? SqlString.Null) ; ;
                
                cmd.Parameters.AddRange(p);
                cnn.Open();

              int i=  cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public List<Employee> SelectAll()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select EmployeeID , FirstName,LastName,Title,BirthDate,HireDate,Country,Notes from Employees order by EmployeeID";
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();

                while (reader.Read())
                {
                    Employee item = new Employee
                    { 
                    EmployeeID=reader.GetInt32(0),
                    FirstName=reader.GetString(1),
                    LastName=reader.GetString(2),
                    Title=reader.GetString(3),
                    BirthDate=reader.GetDateTime(4),
                    HireDate=reader.GetDateTime(5),
                    Country=reader.GetString(6),
                    Notes=reader.IsDBNull(7) ? "": reader.GetString(7)

                    };
                    employees.Add(item);
                }
                reader.Close();
                cnn.Close();
                return employees;
            }
        }

        public Employee SelectByID(int id)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select EmployeeID , FirstName,LastName,Title,BirthDate,HireDate,Country,Notes from Employees EmployeeID=@EmployeeID";
                SqlParameter p = new SqlParameter(@"EmployeeID", id);
                cmd.Parameters.Add(p);
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();

                while (reader.Read())
                {
                    Employee item = new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Title = reader.GetString(3),
                        BirthDate = reader.GetDateTime(4),
                        HireDate = reader.GetDateTime(5),
                        Country = reader.GetString(6),
                        Notes = reader.IsDBNull(7)?"": reader.GetString(7) ?? ""

                    };
                    employees.Add(item);
                }
                reader.Close();
                cnn.Close();
                return employees.SingleOrDefault();
            }


        }

        public List<string> SelectCountries()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select distinct country from Employees order by country";
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> countries = new List<string>();

                while (reader.Read())
                {
                    string item = reader.GetString(0);
                    countries.Add(item);
                }
                reader.Close();
                cnn.Close();
                return countries;
            }
        }

        public void Update(Employee emp)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employees set FirstName=@FirstName,LastName=@LastName,Title=@Title,BirthDate=@BirthDate,HireDate=@HireDate,Country=@Country,Notes=@Notes where " +
                    "EmployeeID=@EmployeeID";

                SqlParameter[] p = new SqlParameter[8];
                p[0] = new SqlParameter("@FirstName", emp.FirstName);
                p[1] = new SqlParameter("@LastName", emp.LastName);
                p[2] = new SqlParameter("@Title", emp.Title);
                p[3] = new SqlParameter("@BirthDate", emp.BirthDate);
                p[4] = new SqlParameter("@HireDate", emp.HireDate);
                p[5] = new SqlParameter("@Country", emp.Country);
                p[6] = new SqlParameter("@Notes", emp.Notes ?? SqlString.Null); ;
                p[7] = new SqlParameter("@EmployeeID", emp.EmployeeID);

                cmd.Parameters.AddRange(p);
                cnn.Open();

                int i = cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
