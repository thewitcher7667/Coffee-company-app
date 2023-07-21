using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Contracts
{
    public interface IRestaurantServices
    {
        public Task<List<CoffeItem>> GetCoffeItems();

        public CoffeItem GetCoffeItem(int id);

        public bool EditCoffeItem(CoffeItem item);  

        public Task<CoffeItem> AddCoffeItem(CoffeItem coffeItem);

        public bool DeleteCoffeItem(int id);

        public bool EditCoffeeItemQuantity(int quantity,int Id);
    }
}
