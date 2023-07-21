using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Contracts
{
    public interface IItemBuyedServices
    {
        ItemsBuyed GetById(int id);

        List<ItemsBuyed> GetByResetId(int resetId);

        Task<ItemsBuyed> Add(ItemsBuyed item);
    }
}
