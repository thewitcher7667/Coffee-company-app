using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.DataContext;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class ResetServices : IResetServices
    {
        private readonly IGenaricRespository<Reset> genaricRespository;
        private readonly IItemBuyedServices itemBuyedServices;
        private readonly IUserServices userServices;
        private readonly AppDbContext appDbContext;

        public ResetServices(IGenaricRespository<Reset> genaricRespository,
            IItemBuyedServices itemBuyedServices,
            IUserServices userServices,
            AppDbContext appDbContext
            )
        {
            this.genaricRespository = genaricRespository;
            this.itemBuyedServices = itemBuyedServices;
            this.appDbContext = appDbContext;
            this.userServices = userServices;
        }

        public async Task<Reset> AddReset(List<ItemsBuyed> items, string userId)
        {
            var user = userServices.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("This user now match our records");
            }
            decimal totalPrice = 0;
            Reset r = new Reset() { UserId = userId };
            await genaricRespository.AddAsync(r);
            for (int i=0;i<items.Count; i++)
            {
                items[i].ResetId = r.Id;
                ItemsBuyed item  = await itemBuyedServices.Add(items[i]);
                totalPrice += item.Price;
            }
            r.TotalPrice = totalPrice;
            Update(r);
            return r;
        }

        public async Task<List<Reset>> GetResets()
        {
            return await appDbContext.Resets.Include(x=>x.ItemsBuyed).ThenInclude(x=>x.CoffeItem).ToListAsync();
        }

        public async Task<List<Reset>> GetResetListByUserId(string userId)
        {

            return await appDbContext.Resets.Include(x => x.ItemsBuyed).ThenInclude(x => x.CoffeItem).Where(x=>x.UserId == userId).ToListAsync();
        }

        public Reset GetReset(int id)
        {
            return appDbContext.Resets.Include(x => x.ItemsBuyed).ThenInclude(x => x.CoffeItem).Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Update(Reset reset)
        {
            return genaricRespository.Update(reset);
        }
    }
}
