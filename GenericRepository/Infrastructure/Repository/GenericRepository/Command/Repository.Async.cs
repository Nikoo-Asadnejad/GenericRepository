using System.Linq.Expressions;
using GenericRepository.Application.Interfaces.GenericRepository.Command;
using GenericRepository.Domain;
using GenericRepository.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

  public async Task ExecuteDeleteAsync(Expression<Func<T, bool>> condition)
  {
    
  }
  public async Task UpdateAsync(T model)
  {
    _context.Attach(model);
    _context.Entry(model).State = EntityState.Modified;
  }
  public async Task UpdateRangeAsync(IEnumerable<T> models)
  => _model.UpdateRange(models);
  
   
  










}

