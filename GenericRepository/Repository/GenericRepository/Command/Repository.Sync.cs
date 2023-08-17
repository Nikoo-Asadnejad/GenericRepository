using GenericReositoryDll.Enumrations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GenericRepository.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Repository;

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

  
}