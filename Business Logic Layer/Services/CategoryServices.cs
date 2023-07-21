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
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenaricRespository<Category> _genaricRespository;

        public CategoryServices(IGenaricRespository<Category> genaricRespository)
        {
            _genaricRespository = genaricRespository;
        }

        public async Task<Category> AddCategory(Category category)
        {
           return await _genaricRespository.AddAsync(category);
        }

        public bool DeleteCategory(int id)
        {
            return _genaricRespository.Delete(id);
        }

        public bool EditCategory(Category category)
        {
            return _genaricRespository.Update(category);    
        }

        public async Task<List<Category>> GetAllCategories()
        {
           return await _genaricRespository.GetAllAsync();
        }

        public Category GetCategory(int id)
        {
            return _genaricRespository.GetBy(x=>x.Id == id).FirstOrDefault();
        }
    }
}
