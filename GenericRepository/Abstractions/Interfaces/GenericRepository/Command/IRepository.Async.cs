using System.Linq.Expressions;
using GenericRepository.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Abstractions.Interfaces.GenericRepository.Command;

public partial interface IRepository<T> where T : BaseEntity
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
    Task CommitTransactionAsync();
    Task AddAsync(T model);
    Task AddRangeAsync(IEnumerable<T> models);
    Task UpdateAsync(T model);
    Task UpdateRangeAsync(IEnumerable<T> models);
    Task DeleteAsync(long id);
    Task DeleteAsync(T model);
    Task DeleteRangeAsync(IEnumerable<T> models);
    Task ExecuteDeleteAsync(Expression<Func<T, bool>> condition);

}