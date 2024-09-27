using System.Linq.Expressions;
using GenericRepository.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Infrastructure.Repository.GenericRepository.Command;

public sealed partial class Repository<T> where T : BaseEntity
{
    public IDbContextTransaction BeginTransaction()
    {
        IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction();
        return dbContextTransaction;
    }
    public void RollbackTransaction()
        => _context.Database.RollbackTransaction();
    public void CommitTransaction()
        => _context.Database.CommitTransaction();

    public void Add(T model)
        => _model.Add(model);
    
    public void AddRange(IEnumerable<T> models)
        => _model.AddRange(models);
    
    public void Delete(long id)
    {
        T model = _context.Find<T>(id);
        if (model != null) Delete(model);
    }
    
    public void Delete(T model)
    {
        if (_context.Entry(model).State is EntityState.Detached)
            _model.Attach(model);

        _model.Remove(model);
    }
    
    public void DeleteRange(IEnumerable<T> models)
        => _model.RemoveRange(models);
    
    public void Update(T model)
    {
        _context.Attach(model);
        _context.Entry(model).State = EntityState.Modified;
    }
    
    public void UpdateRange(IEnumerable<T> models)
        => _model.UpdateRange(models);
    
    public void ClearChangeTracker()
        => _context.ChangeTracker.Clear();

    public void ExecuteDelete(Expression<Func<T, bool>> condition)
    {
        _model.Where(condition)
              .ExecuteDelete();
    }
    
    public void ExecuteUpdate(Expression<Func<T, bool>> condition,
        Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
    {
        _model.Where(condition)
            .ExecuteUpdate(updateExpression);
    }
    
    public void ExecuteUpdate(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
    {
        _model.ExecuteUpdate(updateExpression);
    }

    public void TruncateTable()
    {
        var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();
         _context.Database.ExecuteSqlRaw($"TRUNCATE TABLE {tableName}");
    }
}