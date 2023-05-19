using GenericReositoryDll.Enumrations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GenericRepository.Models;

namespace GenericRepository.Repository;

public partial class Repository<T>  where T : BaseModel
{

  public void Add(T model)
  => _model.Add(model);
  
  public void AddRange(IEnumerable<T> models)
  => _model.AddRange(models);
  
  public bool Any(Expression<Func<T, bool>> query)
  => _model.Any(query);
  
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
  
  public long GetCount(Expression<Func<T, bool>>? query = null)
   => _model.Count(query);

  public List<TResult> GetList<TResult>(Expression<Func<T, bool>>? query,
    Func<T, TResult> selector,
    Func<T, object>? orderBy = null,
    OrderType? orderType = null,
    List<string>? includes = null,
    int? skip = null,
    int? take = null,
    bool? distinct = null,
    bool asTracking = false)
  {
    List<TResult> result;
    var models = _model.AsQueryable();

    if (!asTracking)
      models.AsNoTrackingWithIdentityResolution();

    if (includes != null && includes.Count() > 0)
      includes.ForEach(includeProperty => models.Include(includeProperty));


    models = models.Where(query);

    if (skip != null)
      models = models.Skip((int)skip);

    if (take != null)
      models = models.Take((int)take);

    if (orderBy != null && orderType == OrderType.Asc)
      models = models.OrderBy(orderBy).AsQueryable();

    if (orderBy != null && orderType == OrderType.Desc)
      models = models.OrderByDescending(orderBy).AsQueryable();

    if (orderBy != null && orderType == null)
      models = models.OrderBy(orderBy).AsQueryable();

    if (distinct != null)
      models.Distinct();

    result = models.Select(selector).ToList();

    return result;

  }

  public List<T> GetList(Expression<Func<T, bool>>? query = null,
   Func<T, object>? orderBy = null,
   OrderType? orderType = null,
   List<string>? includes = null,
   int? skip = 0,
   int? take = null,
   bool? distinct = null,
   bool asTracking = false)
  {
    var models = _model.AsQueryable();

    if (!asTracking)
      models.AsNoTrackingWithIdentityResolution();

    if (includes != null && includes.Count() > 0)
      includes.ForEach(includeProperty => models.Include(includeProperty));

    if (query != null)
      models = models.Where(query);

    if (skip != null)
      models = models.Skip((int)skip);

    if (take != null)
      models = models.Take((int)take);

    if (orderBy != null && orderType == OrderType.Asc)
      models = models.OrderBy(orderBy).AsQueryable();

    if (orderBy != null && orderType == OrderType.Desc)
      models = models.OrderByDescending(orderBy).AsQueryable();

    if (orderBy != null && orderType == null)
      models = models.OrderBy(orderBy).AsQueryable();

    if (distinct != null)
      models.Distinct();

    return models.ToList();
  }


  public List<TResult> GetAll<TResult>(Func<T, TResult> selector,
    Func<T, object>? orderBy = null,
    OrderType? orderType = null,
    List<string>? includes = null,
    int? skip = null,
    int? take = null,
    bool? distinct = null,
    bool asTracking = false)
  {
    List<TResult> result;
    var models = _model.AsQueryable();

    if (!asTracking)
      models.AsNoTrackingWithIdentityResolution();

    if (includes != null && includes.Count() > 0)
      includes.ForEach(includeProperty => models.Include(includeProperty));

    if (skip != null)
      models = models.Skip((int)skip);

    if (take != null)
      models = models.Take((int)take);

    if (orderBy != null && orderType == OrderType.Asc)
      models = models.OrderBy(orderBy).AsQueryable();

    if (orderBy != null && orderType == OrderType.Desc)
      models = models.OrderByDescending(orderBy).AsQueryable();

    if (orderBy != null && orderType == null)
      models = models.OrderBy(orderBy).AsQueryable();

    if (distinct != null)
      models.Distinct();

    result = models.Select(selector).ToList();

    return result;

  }


  public List<T> GetAll(Func<T, object>? orderBy = null,
  OrderType? orderType = null,
  List<string>? includes = null,
  int? skip = 0,
  int? take = null,
  bool? distinct = null,
  bool asTracking = false)
  => GetList(null, orderBy, orderType, includes, skip, take, distinct,asTracking);

  public TResult GetSingle<TResult>(Expression<Func<T, bool>>? query,
  Func<T, TResult> selector,
  List<string>? includes = null,
    bool asTracking = false)
  {
    TResult result;

    IQueryable<T> model = _model.AsQueryable();

    if (!asTracking)
      model.AsNoTrackingWithIdentityResolution();

    if (includes != null && includes.Count() > 0)
      includes.ForEach(includeProperty => model.Include(includeProperty));


    model = model.Where(query);

    result = model.Select(selector).FirstOrDefault();

    return result;

  }

  public T GetSingle(Expression<Func<T, bool>>? query,
  List<string>? includes = null,
    bool asTracking = false)

  {
    T result;
    var model = _model.AsQueryable();

    if (!asTracking)
      model.AsNoTrackingWithIdentityResolution();

    if (includes != null && includes.Count() > 0)
      includes.ForEach(includeProperty => model.Include(includeProperty));

    model = model.Where(query);

    result = model.FirstOrDefault();

    return result;
  }

  public T Find(long id)
  => _model.Find(id);

  public void Update(T model)
  {
    _context.Attach(model);
    _context.Entry(model).State = EntityState.Modified;
  }
  
  public void UpdateRange(IEnumerable<T> models)
  => _model.UpdateRange(models);
  

}

