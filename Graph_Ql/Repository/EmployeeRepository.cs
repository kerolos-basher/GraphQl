using Dapper;
using Graph_Ql.Data;
using Graph_Ql.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<Employee>> GetEmployees()
        {
            IEnumerable<Employee> Emplist;
            using (SqlConnection connection = new SqlConnection(connecinstring))
            {
                await connection.OpenAsync();
                var sqlqury = "Select * from Employee";
                Emplist = await connection.QueryAsync<Employee>(sqlqury);
            }
            return Emplist.ToList();
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
}
