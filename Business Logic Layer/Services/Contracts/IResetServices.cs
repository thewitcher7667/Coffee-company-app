using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Contracts
{
    public interface IResetServices
    {
        Task<List<Reset>> GetResets(); 

        Reset GetReset(int id);

        Task<List<Reset>> GetResetListByUserId(string userId);

        Task<Reset> AddReset(List<ItemsBuyed> items,string userId);

        bool Update(Reset reset);
    }
}
