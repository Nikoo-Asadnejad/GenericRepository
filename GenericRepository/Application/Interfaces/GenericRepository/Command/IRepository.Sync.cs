using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Application.Interfaces.GenericRepository.Command;

public partial interface IRepository<T>
{
    IDbContextTransaction BeginTransaction();
    void RollbackTransaction();
    void CommitTransaction();
    void Add(T model);
    void AddRange(IEnumerable<T> models);
    void Update(T model);
    void UpdateRange(IEnumerable<T> models);
    void Delete(long id);
    void Delete(T model);
    void DeleteRange(IEnumerable<T> models);
    void ClearChangeTracker();
    void ExecuteDelete(Expression<Func<T, bool>> condition);
    void ExecuteUpdate(Expression<Func<T, bool>> condition,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
    void ExecuteUpdate(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
}