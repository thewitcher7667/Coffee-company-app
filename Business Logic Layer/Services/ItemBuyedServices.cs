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
    public class ItemBuyedServices : IItemBuyedServices
    {
        private readonly IGenaricRespository<ItemsBuyed> genaricRespository;
        private readonly IRestaurantServices restaurantServices;


        public ItemBuyedServices(IGenaricRespository<ItemsBuyed> genaricRespository, IRestaurantServices restaurantServices)
        {
            this.genaricRespository = genaricRespository;
            this.restaurantServices = restaurantServices;
        }

        public async Task<ItemsBuyed> Add(ItemsBuyed item)
        {
            CoffeItem coffeItem = restaurantServices.GetCoffeItem(item.CoffeItemId);
            item.Price = item.Quantity * coffeItem.Price;

           int newQ = coffeItem.Quantity - item.Quantity;
            restaurantServices.EditCoffeeItemQuantity(newQ,coffeItem.Id);

            return await genaricRespository.AddAsync(item);
        }

        public ItemsBuyed GetById(int id)
        {
            return genaricRespository.GetBy(x=>x.Id == id).FirstOrDefault();
        }

        public List<ItemsBuyed> GetByResetId(int resetId)
        {
            return genaricRespository.GetBy(x => x.ResetId == resetId);
        }
    }
}
