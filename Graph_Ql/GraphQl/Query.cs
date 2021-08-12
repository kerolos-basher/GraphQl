using Graph_Ql.Model;
using Graph_Ql.Repository;
using HotChocolate;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph_Ql.GraphQl
{
    public class Query
    {
        public async Task<List<Employee>> AllEmployeeOnly([Service] EmployeeRepository employeeRepository) =>
          await employeeRepository.GetEmployees();

        public List<Employee> AllEmployeeWithDepartment([Service] EmployeeRepository employeeRepository) =>
            employeeRepository.GetEmployeesWithDepartment();

        public async Task<Employee> GetEmployeeById([Service] EmployeeRepository employeeRepository,
            [Service] ITopicEventSender eventSender, int id)
        {
            Employee gottenEmployee = await employeeRepository.GetEmployeeById(id);
            await eventSender.SendAsync("ReturnedEmployee", gottenEmployee);
            return gottenEmployee;
        }

        public List<Department> AllDepartmentsOnly([Service] DepartmentRepository departmentRepository) =>
            departmentRepository.GetAllDepartmentOnly();

        public List<Department> AllDepartmentsWithEmployee([Service] DepartmentRepository departmentRepository) =>
            departmentRepository.GetAllDepartmentsWithEmployee();
    }
}
