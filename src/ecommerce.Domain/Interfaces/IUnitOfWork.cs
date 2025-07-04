using ecommerce.Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();

    }
}
