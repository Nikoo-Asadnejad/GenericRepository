using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Abstractions.Interfaces.UnitOfWork;

public interface IUnitOfWork 
{ 
    Task<int> SaveAsync();
    int Save();
    Task DisposeAsync();
    void Dispose();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
    Task CommitTransactionAsync();
}