using System.Linq.Expressions;
using GenericRepository.Abstractions.Interfaces.GenericRepository.Query;
using GenericRepository.Entities;
using GenericRepository.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Repository.GenericRepository.Query;

public sealed partial class QueryGenericRepository<T> : IQueryGenericRepository<T> where  T : BaseEntity
{
    

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
        IQueryable<T> models = _model.AsQueryable();

        if (!asTracking)
            models.AsNoTrackingWithIdentityResolution();

        if (includes is not null && includes.Any())
            includes.ForEach(includeProperty => models.Include(includeProperty));


        models = models.Where(query);

        if (skip is not null)
            models = models.Skip((int)skip);

        if (take is not null)
            models = models.Take((int)take);

        if (orderBy is not null && orderType is OrderType.Asc)
            models = models.OrderBy(orderBy).AsQueryable();

        if (orderBy is not null && orderType is OrderType.Desc)
            models = models.OrderByDescending(orderBy).AsQueryable();

        if (orderBy is not null && orderType == null)
            models = models.OrderBy(orderBy).AsQueryable();

        if (distinct is not null)
            models = models.Distinct();

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
        IQueryable<T> models = _model.AsQueryable();

        if (!asTracking)
            models.AsNoTrackingWithIdentityResolution();

        if (includes is not null && includes.Any())
            includes.ForEach(includeProperty => models.Include(includeProperty));

        if (query is not null)
            models = models.Where(query);

        if (skip is not null)
            models = models.Skip((int)skip);

        if (take is not null)
            models = models.Take((int)take);

        if (orderBy is not null && orderType is OrderType.Asc)
            models = models.OrderBy(orderBy).AsQueryable();

        if (orderBy is not null && orderType is OrderType.Desc)
            models = models.OrderByDescending(orderBy).AsQueryable();

        if (orderBy is not null && orderType is null)
            models = models.OrderBy(orderBy).AsQueryable();

        if (distinct != null)
            models = models.Distinct();
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
        IQueryable<T> models = _model.AsQueryable();

        if (!asTracking)
            models.AsNoTrackingWithIdentityResolution();

        if (includes is not null && includes.Any())
            includes.ForEach(includeProperty => models.Include(includeProperty));

        if (skip is not null)
            models = models.Skip((int)skip);

        if (take is not null)
            models = models.Take((int)take);

        if (orderBy is not null && orderType is OrderType.Asc)
            models = models.OrderBy(orderBy).AsQueryable();

        if (orderBy is not null && orderType is OrderType.Desc)
            models = models.OrderByDescending(orderBy).AsQueryable();

        if (orderBy is not null && orderType is null)
            models = models.OrderBy(orderBy).AsQueryable();

        if (distinct is not null)
            models = models.Distinct();

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
        => GetList(null, orderBy, orderType, includes, skip, take, distinct, asTracking);

    public TResult GetSingle<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        List<string>? includes = null,
        bool asTracking = false)
    {
        TResult result;

        IQueryable<T> model = _model.AsQueryable();

        if (!asTracking)
            model.AsNoTrackingWithIdentityResolution();

        if (includes is not null && includes.Any())
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
        IQueryable<T> model = _model.AsQueryable();

        if (!asTracking)
            model.AsNoTrackingWithIdentityResolution();

        if (includes is not null && includes.Any())
            includes.ForEach(includeProperty => model.Include(includeProperty));

        model = model.Where(query);
        result = model.FirstOrDefault();
        return result;
    }

    public IQueryable<T> GetQueryable()
        => _model.AsQueryable();

    public T Find(long id)
        => _model.Find(id);

    public bool Any(Expression<Func<T, bool>> query)
        => _model.Any(query);
}