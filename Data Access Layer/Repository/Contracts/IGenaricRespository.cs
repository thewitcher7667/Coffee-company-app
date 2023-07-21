using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Contracts
{
    public interface IGenaricRespository<T> where T : class
    {
        Task<List<T>> GetAllAsyncWithIclude<TResult>(Expression<Func<T, TResult>> predicate) where TResult : class;

        bool Delete<TId>(TId entityId);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        bool Update(T entity);

        List<T> GetBy(Expression<Func<T,bool>> predicate);

        List<T> GetByIclude<TId>(Expression<Func<T, bool>> predicate, Expression<Func<T, TId>> predicateIclude) where TId : class;
    }
}
