using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.Contracts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly IGenaricRespository<CoffeItem> genaricRespository;

        public RestaurantServices(IGenaricRespository<CoffeItem> genaricRespository)
        {
            this.genaricRespository = genaricRespository;
        }

        public async Task<CoffeItem> AddCoffeItem(CoffeItem coffeItem)
        {
                var newCoffee = await genaricRespository.AddAsync(coffeItem);
            return GetCoffeItem(newCoffee.Id);
        }

        public bool DeleteCoffeItem(int id)
        {
            return genaricRespository.Delete(id);
        }

        public bool EditCoffeItem(CoffeItem item)
        {
            return genaricRespository.Update(item);
        }

        public CoffeItem GetCoffeItem(int id)
        {
           return genaricRespository.GetByIclude(x=>x.Id==id,y=>y.Category).FirstOrDefault();
        }

        public async Task<List<CoffeItem>> GetCoffeItems()
        {
            return await genaricRespository.GetAllAsyncWithIclude<Category>(x=>x.Category);
        }

        public bool EditCoffeeItemQuantity(int quantity,int id)
        {
            var coffeeItem = genaricRespository.GetBy(x=>x.Id == id).FirstOrDefault();
            coffeeItem.Quantity = quantity;

            return EditCoffeItem(coffeeItem);
        }
    }
}
