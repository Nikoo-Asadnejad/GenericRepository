using System.Globalization;
using GenericReositoryDll.Enumrations;
using System.Linq.Expressions;
using GenericRepository.Models;

namespace GenericRepository.Interfaces.Repository;

public partial interface IRepository<T> where T : BaseModel
{


    Task AddAsync(T model);

    Task AddRangeAsync(IEnumerable<T> models);

    Task UpdateAsync(T model);

    Task UpdateRangeAsync(IEnumerable<T> models);

    Task DeleteAsync(long id);

    Task DeleteAsync(T model);

    Task DeleteRangeAsync(IEnumerable<T> models);

 
}