using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.Contracts
{
    public  interface ICategoryServices
    {
        Category GetCategory(int id);

        Task<List<Category>> GetAllCategories();

        bool EditCategory(Category category);

        bool DeleteCategory(int id);

        Task<Category> AddCategory (Category category);    

    }
}
