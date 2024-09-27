using System.Linq.Expressions;
using GenericRepository.Application.Interfaces.GenericRepository.Command;
using GenericRepository.Domain;
using GenericRepository.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Query;


namespace GenericRepository.Infrastructure.Repository.GenericRepository.Command;

public sealed partial class Repository<T> : IRepository<T> where T : BaseEntity
{

  private readonly CommandContext _context;
  private readonly DbSet<T> _model;
  public Repository(CommandContext context)
  {
    this._context = context;
    _model = _context.Set<T>();
  }
  
  public async Task AddAsync(T model)
  => await _model.AddAsync(model);
  
  public async Task AddRangeAsync(IEnumerable<T> models)
  => await _model.AddRangeAsync(models);
  
  public async Task DeleteAsync(long id)
  {
    T? model = await _context.FindAsync<T>(id);
    if (model != null) 
      await DeleteAsync(model);
  }
  
  public async  Task DeleteAsync(T model)
  {
    if (_context.Entry(model).State is EntityState.Detached)
      _context.Attach(model);
    
    _model.Remove(model);
  }

  public async Task SoftDeleteAsync(long id)
  {
    T? model = await _context.FindAsync<T>(id);
    if (model is not null) 
      await SoftDeleteAsync(model);
  }
  
  public async Task SoftDeleteAsync(T model)
  {
    model.Delete();
    await UpdateAsync(model);
  }
  
  public async Task DeleteRangeAsync(IEnumerable<T> models)
  => _model.RemoveRange(models);
  
  public async Task SoftDeleteRangeAsync(IEnumerable<T> models)
  {
    foreach (T model in models)
    {
      await SoftDeleteAsync(model);
    }
    
  }
  
  public async Task UpdateAsync(T model)
  {
    _context.Attach(model);
    _context.Entry(model).State = EntityState.Modified;
  }
  
  public async Task UpdateRangeAsync(IEnumerable<T> models)
  => _model.UpdateRange(models);

  public async Task ExecuteDeleteAsync(Expression<Func<T, bool>> condition)
  {
    await _model.Where(condition)
          .ExecuteDeleteAsync();
  }
  
  public async Task ExecuteUpdateAsync(Expression<Func<T, bool>> condition,
    Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
  {
    await _model.Where(condition)
      .ExecuteUpdateAsync(updateExpression);
  }
    
  public async Task ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
  {
    await _model.ExecuteUpdateAsync(updateExpression);
  }

  public async Task TruncateTableAsync()
  {
    var tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();
    await _context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {tableName}");
  }
  
  
}

