using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class DepartmentServices : IDepartmenServices
    {
        private readonly IGenaricRespository<Department> genaricRespository;

        public DepartmentServices(IGenaricRespository<Department> genaricRespository)
        {
            this.genaricRespository = genaricRespository;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            return await genaricRespository.AddAsync(department);
        }

        public bool CheckIfExist(int id)
        {
            var department = GetDepartmentById(id);
            if(department == null)
            {
                return false;
            }
            return true;
        }

        public bool DeleteDepartment(int id)
        {

            return genaricRespository.Delete<int>(id);
        }

        public Department GetDepartmentById(int id)
        {
            return genaricRespository.GetBy(x=>x.Id == id).FirstOrDefault();
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await genaricRespository.GetAllAsync();
        }

        public bool UpdateDepartment(Department department)
        {
            return genaricRespository.Update(department);
        }
    }
}
