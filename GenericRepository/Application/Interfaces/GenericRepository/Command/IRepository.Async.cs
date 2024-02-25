using System.Linq.Expressions;
using GenericRepository.Domain;

namespace GenericRepository.Application.Interfaces.GenericRepository.Command;

public partial interface IRepository<T> where T : BaseEntity
{

    Task AddAsync(T model);
    Task AddRangeAsync(IEnumerable<T> models);
    Task UpdateAsync(T model);
    Task UpdateRangeAsync(IEnumerable<T> models);
    Task DeleteAsync(long id);
    Task DeleteAsync(T model);
    Task SoftDeleteAsync(long id);
    Task SoftDeleteAsync(T model);
    Task DeleteRangeAsync(IEnumerable<T> models);
    Task SoftDeleteRangeAsync(IEnumerable<T> models);
    Task ExecuteDeleteAsync(Expression<Func<T, bool>> condition);

}