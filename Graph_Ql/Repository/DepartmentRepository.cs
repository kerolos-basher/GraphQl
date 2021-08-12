using Graph_Ql.Data;
using Graph_Ql.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graph_Ql.Repository
{
    public class DepartmentRepository
    {
        private readonly ApplicationDbContext _sampleAppDbContext;

        public DepartmentRepository(ApplicationDbContext sampleAppDbContext)
        {
            _sampleAppDbContext = sampleAppDbContext;
        }

        public List<Department> GetAllDepartmentOnly()
        {
            return _sampleAppDbContext.Department.ToList();
        }

        public List<Department> GetAllDepartmentsWithEmployee()
        {
            return _sampleAppDbContext.Department
                .Include(d => d.Employees)
                .ToList();
        }

        public async Task<Department> CreateDepartment(Department department)
        {
            await _sampleAppDbContext.Department.AddAsync(department);
            await _sampleAppDbContext.SaveChangesAsync();
            return department;
        }
    }
}
