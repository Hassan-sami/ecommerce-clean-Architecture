using ecommerce.Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        ValueTask<TEntity> AddAsync(TEntity entity);
        ValueTask<IEnumerable<TEntity>> GetAll();
        ValueTask<TEntity> FindById(Guid id);
        ValueTask<TEntity> RemoveAsync(Guid id);
    }
}
