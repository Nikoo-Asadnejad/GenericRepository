using GenericRepository.Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Infrastructure.Repository.UnitOfWork;

public class UnitOfWork :  IUnitOfWork
{
    private readonly DbContext _context;
    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync()
    {
        int result = await _context.SaveChangesAsync();
        return result;
    }
    
    public int Save()
        => _context.SaveChanges();
    
    public async Task DisposeAsync()
        => await _context.DisposeAsync();
    
    public void Dispose()
        => _context.Dispose();
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        IDbContextTransaction dbContextTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return dbContextTransaction;
    }
    
    public async Task RollbackTransactionAsync()
        =>await _context.Database.RollbackTransactionAsync();
    
    public async Task CommitTransactionAsync()
        => await _context.Database.CommitTransactionAsync();
    
}