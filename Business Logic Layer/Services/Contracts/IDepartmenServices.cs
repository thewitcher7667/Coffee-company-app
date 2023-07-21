using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Contracts
{
    public interface IDepartmenServices
    {
        public Task<Department> AddDepartment(Department department);

        public bool UpdateDepartment(Department department);

        public bool DeleteDepartment(int id);

        public Task<List<Department>> GetDepartments();

        public Department GetDepartmentById(int id);
    }
}
