using GenericReositoryDll.Enumrations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Interfaces.Repository;

public partial interface IRepository<T>
{
    IDbContextTransaction BeginTransaction();
    void Add(T model);
    void AddRange(IEnumerable<T> models);
    void Update(T model);
    void UpdateRange(IEnumerable<T> models);
    void Delete(long id);
    void Delete(T model);
    void DeleteRange(IEnumerable<T> models);
    

}