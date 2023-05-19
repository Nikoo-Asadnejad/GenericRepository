using System.Linq.Expressions;
using GenericReositoryDll.Enumrations;
using GenericRepository.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Repository;

public partial class QueryGenericRepository<T> : IQueryGenericRepository<T>
{
    private readonly DbContext _context;
    private readonly DbSet<T> _model;

    public QueryGenericRepository(DbContext context)
    {
        this._context = context;
        _model = _context.Set<T>();
    }

    public async Task<long> GetCountAsync(Expression<Func<T, bool>> query = null)
        => await _model.CountAsync(query);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query">Get where query</param>
    /// <param name="orderBy">Order by orderBy expression</param>
    /// <param name="orderByType">Desc | Asc </param>
    /// <param name="skip">skip</param>
    /// <param name="take">take</param>
    /// <returns></returns>
    public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        Func<T, object> orderBy = null,
        OrderType? orderType = null,
        List<string> includes = null,
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

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> query = null,
        Func<T, object> orderBy = null,
        OrderType? orderType = null,
        List<string> includes = null,
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

    public async Task<List<TResult>> GetAllAsync<TResult>(Func<T, TResult> selector,
        Func<T, object> orderBy = null,
        OrderType? orderType = null,
        List<string> includes = null,
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

    public async Task<List<T>> GetAllAsync(
        Func<T, object> orderBy = null,
        OrderType? orderType = null,
        List<string> includes = null,
        int? skip = 0,
        int? take = null,
        bool? distinct = null,
        bool asTracking = false)
        => await GetListAsync(null, orderBy, orderType, includes, skip, take, distinct, asTracking);

    public async Task<IQueryable<T>> GetQueriableAsync()
        => _model.AsQueryable();

    public async Task<TResult> GetSingleAsync<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        List<string> includes = null,
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

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>>? query,
        List<string> includes = null,
        bool asTracking = false)
    {
        T result;
        var model = _model.AsQueryable();

        if (!asTracking)
            model.AsNoTrackingWithIdentityResolution();

        if (includes != null && includes.Count() > 0)
            includes.ForEach(includeProperty => model.Include(includeProperty));

        model = model.Where(query);

        result = await model.FirstOrDefaultAsync();

        return result;
    }

    public async Task<T> FindAsync(long id)
        => await _model.FindAsync(id);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
        => await _model.AnyAsync(query);
}