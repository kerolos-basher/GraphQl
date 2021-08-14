using Dapper;
using Graph_Ql.Data;
using Graph_Ql.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Dynamic;

namespace Graph_Ql.Repository
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _sampleAppDbContext;
        private readonly string connecinstring = "Server=DESK3\\SQLEXPRESS;Database=aspnet-Graph_Ql;Trusted_Connection=True;MultipleActiveResultSets=true";
        public EmployeeRepository(ApplicationDbContext sampleAppDbContext)
        {
            _sampleAppDbContext = sampleAppDbContext;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> Emplist;
            using (SqlConnection connection = new SqlConnection(connecinstring))
            {
                //connection.Open();
                Emplist = connection.GetAll<Employee>().ToList();
            }
            return Emplist;
        }
        public EmpWithProductDto GetEmployeesWithProducts()
        {
            dynamic obj = new ExpandoObject();
            using (SqlConnection connection = new SqlConnection(connecinstring))
            {
                // connection.Open();
                var qury = @"Select * From Employee;
                            Select * From products";
                using (var mul = connection.QueryMultiple(qury, null))
                {
                    obj.Employee = mul.Read<Employee>().ToList();
                    obj.products = mul.Read<Products>().ToList();
                }

            }
            return new EmpWithProductDto();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee Emp;
            using (SqlConnection connection = new SqlConnection(connecinstring))
            {
                await connection.OpenAsync();
                var sqlqury = "Select * from Employee where EmployeeId = @ID ";
                Emp = await connection.QuerySingleAsync<Employee>(sqlqury, new { ID = id });
            }
            return Emp;
        }

        public List<Employee> GetEmployeesWithDepartment()
        {
            return _sampleAppDbContext.Employee
                .Include(e => e.Department)
                .ToList();
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            employee.EmployeeId = 0;
            await _sampleAppDbContext.Employee.AddAsync(employee);
            await _sampleAppDbContext.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> UpdateEmployee(int employeeId, string name, int age, string email)
        {

            Employee UpdatedEmployee = _sampleAppDbContext.Employee
                .Include(e => e.Department)
                .Where(e => e.EmployeeId == employeeId)
                .FirstOrDefault();
            UpdatedEmployee.Name = name;
            UpdatedEmployee.Age = age;
            UpdatedEmployee.Email = email;
            UpdatedEmployee.EmployeeId = employeeId;
            await _sampleAppDbContext.SaveChangesAsync();
            return UpdatedEmployee;
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            _sampleAppDbContext.Remove(employee);
            await _sampleAppDbContext.SaveChangesAsync();
            return true;
        }
    }
    public class EmpWithProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset IntroducedAt { get; set; }
        public string PhotoFileName { get; set; }

        public double Price { get; set; }
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public int DepartmentId { get; set; }
    }
}
