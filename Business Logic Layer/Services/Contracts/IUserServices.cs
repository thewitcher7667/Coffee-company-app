using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Contracts
{
    public interface IUserServices
    {
        public Task<List<User>> GetUsers();

        public User GetUserById(string id);

        User GetUserByEmail(string email);  
        
        public Task<User> AddUser(User user);
        
        public Task<bool> UpdatUser(User user);

        public bool DeleteUser(string id);
    }
}
