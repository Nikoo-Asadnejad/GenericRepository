using System.Data;
using System.Globalization;
using GenericReositoryDll.Enumrations;
using System.Linq.Expressions;
using GenericRepository.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Interfaces.Repository;

public partial interface IRepository<T> where T : BaseModel
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

 
}