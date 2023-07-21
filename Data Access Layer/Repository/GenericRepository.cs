using Data_Access_Layer.DataContext;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class GenericRepository<T> : IGenaricRespository<T> where T : class
    {
        private readonly AppDbContext context;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public bool Delete<TId>(TId entityId)
        {
            try
            {

                context.Set<T>().Remove(context.Set<T>().Find(entityId));
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<T>> GetAllAsyncWithIclude<TResult>(Expression<Func<T, TResult>> predicate) where TResult : class
        {
            List<T> u = await context.Set<T>().Include(predicate).ToListAsync();
            return u;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public bool Update(T entity)
        {
            try
            {
                context.Update(entity);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }


            return true;
        }

        public List<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public List<T> GetByIclude<TId>(Expression<Func<T, bool>> predicate, Expression<Func<T, TId>> predicateIclude) where TId : class
        {
            return context.Set<T>().Include(predicateIclude).Where(predicate).ToList();
        }
    }
}