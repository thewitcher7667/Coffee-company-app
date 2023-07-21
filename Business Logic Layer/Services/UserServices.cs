using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Business_Logic_Layer.Services
{
    public class UserServices : IUserServices
    {
        private readonly IGenaricRespository<User> genaricRespository;
        private readonly IDepartmenServices departmentRespository;
        private readonly UserManager<User> userManager;

        public UserServices(IGenaricRespository<User> genaricRespository,
            IDepartmenServices departmentRespository,
            UserManager<User> userManager)
        {
            this.genaricRespository = genaricRespository;
            this.departmentRespository = departmentRespository;
            this.userManager = userManager;
        }

        public async Task<User> AddUser(User user)
        {
            //check if the department id exist or not
            if(departmentRespository.GetDepartmentById(user.DepartmentId) == null)
            {
                throw new Exception("This Department Does not Exist");
            }
            else
            {
                var result = await userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                    return GetUserById(user.Id);
              throw new Exception("There is error please try again later");
            }
        }

        public bool DeleteUser(string id)
        {            
            return genaricRespository.Delete<string>(id);
        }

        public User GetUserByEmail(string email)
        {
            return genaricRespository.GetBy(x=>x.Email == email).FirstOrDefault();
        }

        public User GetUserById(string id)
        {
            var r = genaricRespository.GetByIclude(x => x.Id == id, y => y.Department).FirstOrDefault();
            return r;
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> u = await genaricRespository.GetAllAsyncWithIclude<Department>(x=>x.Department);
            return u;
        }

        public async Task<bool> UpdatUser(User user)
        {
            User oldUser = GetUserById(user.Id);

            await userManager.RemoveFromRoleAsync(oldUser, oldUser.Department.Name);

            oldUser.UserName = user.UserName;
            oldUser.Email = user.Email;
            oldUser.DepartmentId = user.DepartmentId;
            oldUser.Department = departmentRespository.GetDepartmentById(user.DepartmentId);

            return genaricRespository.Update(oldUser);
        }

    }
}
