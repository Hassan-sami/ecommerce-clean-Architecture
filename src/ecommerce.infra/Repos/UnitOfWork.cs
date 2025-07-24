using ecommerce.Domain.common;
using ecommerce.Domain.Enitities.Identities;
using ecommerce.Domain.Interfaces;
using ecommerce.infra.Context;

namespace ecommerce.infra.Repos;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool _isDisposed;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing) {
        if(_isDisposed)
            return;
        if (disposing) { // release other disposable objects
            _context.Dispose();
        }
        _isDisposed = true;
    }
    

    

    public Task BeginTransactionAsync()
    {
        _context.Database.BeginTransaction();
        return Task.CompletedTask;
    }

    // public Task SaveChangesAsync()
    // {
    //     
    // }

    public Task CommitAsync()
    {
        _context.Database.CommitTransaction();
        return Task.CompletedTask;
    }

    public Task RollbackAsync()
    {
        _context.Database.RollbackTransaction();
        return Task.CompletedTask;
    }
}