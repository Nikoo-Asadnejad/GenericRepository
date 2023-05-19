using GenericReositoryDll.Enumrations;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using GenericRepository.Interfaces.Repository;
using GenericRepository.Models;

namespace GenericRepository.Repository;

public class CacheRepository<T> : IQueryGenericRepository<T> where T : BaseModel
{
    private readonly IMemoryCache _memoryCache;
    private readonly IQueryGenericRepository<T> _repository;
    private readonly short _expireTime = 1;

    public CacheRepository(IMemoryCache memoryCache, IQueryGenericRepository<T> repository)
    {
        _memoryCache = memoryCache;
        _repository = repository;
    }


    #region Async
    
    public async Task<T> FindAsync(long id)
    {
        string key = GenerateCacheKey(query: id);
        return await _memoryCache.GetOrCreateAsync<T>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.FindAsync(id);
        });
    }
    
    public async Task<List<TResult>> GetAllAsync<TResult>(Func<T, TResult> selector,
        Func<T, object>? orderBy = null,
        OrderType? orderType = null,
        List<string>? includes = null,
        int? skip = null,
        int? take = null,
        bool? distinct = null,
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query: null, selector, orderBy, orderType, includes, skip, take, distinct);
        return await _memoryCache.GetOrCreateAsync<List<TResult>>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.GetAllAsync<TResult>(selector, orderBy, orderType, includes, skip, take, distinct,
                asTracking);
        });
    }

    public async Task<List<T>> GetAllAsync(Func<T, object>? orderBy = null,
        OrderType? orderType = null,
        List<string>? includes = null,
        int? skip = 0,
        int? take = null,
        bool? distinct = null, 
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query: null, selector: null, orderBy, orderType, includes, skip, take, distinct);
        return await _memoryCache.GetOrCreateAsync<List<T>>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.GetAllAsync(orderBy, orderType, includes, skip, take, distinct, asTracking);
        });
    }

    public async Task<long> GetCountAsync(Expression<Func<T, bool>>? query = default)
        => await _repository.GetCountAsync(query);


    public async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        Func<T, object>? orderBy = null, 
        OrderType? orderType = null, 
        List<string>? includes = null,
        int? skip = null,
        int? take = null, 
        bool? distinct = null, 
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector, orderBy, orderType, includes, skip, take, distinct);
        return await _memoryCache.GetOrCreateAsync<List<TResult>>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.GetListAsync<TResult>(query, selector, orderBy, orderType, includes, skip, take,
                distinct, asTracking);
        });
    }

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? query = null,
        Func<T, object>? orderBy = null,
        OrderType? orderType = null,
        List<string>? includes = null,
        int? skip = 0, 
        int? take = null,
        bool? distinct = null, 
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector: null, orderBy, orderType, includes, skip, take, distinct);
        return await _memoryCache.GetOrCreateAsync<List<T>>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.GetListAsync(query, orderBy, orderType, includes, skip, take, distinct,
                asTracking);
        });
    }

    public async Task<TResult> GetSingleAsync<TResult>(Expression<Func<T, bool>>? query, 
        Func<T, TResult> selector,
        List<string>? includes = null,
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector, orderBy: null, orderType: null, includes, skip: null, take: null,
            distinct: null);
        return await _memoryCache.GetOrCreateAsync<TResult>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.GetSingleAsync<TResult>(query, selector, includes, asTracking);
        });
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>>? query, 
        List<string>? includes = null,
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector: null, orderBy: null, orderType: null, includes, skip: null,
            take: null, distinct: null);
        return await _memoryCache.GetOrCreateAsync<T>(key, async (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return await _repository.GetSingleAsync(query, includes, asTracking);
        });
    }

    #endregion

    #region Sync
    
    public T Find(long id)
    {
        string key = GenerateCacheKey(query: id);
        return _memoryCache.GetOrCreate<T>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.Find(id);
        });
    }

    public List<TResult> GetAll<TResult>(Func<T, TResult> selector,
        Func<T, object>? orderBy = null,
        OrderType? orderType = null,
        List<string>? includes = null,
        int? skip = null, int? take = null,
        bool? distinct = null,
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query: null, selector, orderBy, orderType, includes, skip, take, distinct);
        return _memoryCache.GetOrCreate<List<TResult>>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.GetAll<TResult>(selector, orderBy, orderType, includes, skip, take, distinct,
                asTracking);
        });
    }

    public List<T> GetAll(Func<T, object>? orderBy = null,
        OrderType? orderType = null, 
        List<string>? includes = null,
        int? skip = 0, int? take = null,
        bool? distinct = null, 
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query: null, selector: null, orderBy, orderType, includes, skip, take, distinct);
        return _memoryCache.GetOrCreate<List<T>>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.GetAll(orderBy, orderType, includes, skip, take, distinct, asTracking);
        });
    }

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
        string key = GenerateCacheKey(query, selector, orderBy, orderType, includes, skip, take, distinct);
        return _memoryCache.GetOrCreate<List<TResult>>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.GetList<TResult>(query, selector, orderBy, orderType, includes, skip, take, distinct,
                asTracking);
        });
    }

    public List<T> GetList(Expression<Func<T, bool>>? query = null,
        Func<T, object>? orderBy = null,
        OrderType? orderType = null,
        List<string>? includes = null,
        int? skip = 0, int? take = null,
        bool? distinct = null,
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector: null, orderBy, orderType, includes, skip, take, distinct);
        return _memoryCache.GetOrCreate<List<T>>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.GetList(query, orderBy, orderType, includes, skip, take, distinct, asTracking);
        });
    }

    public TResult GetSingle<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        List<string>? includes = null, 
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector, orderBy: null, orderType: null, includes, skip: null, take: null,
            distinct: null);
        return _memoryCache.GetOrCreate<TResult>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.GetSingle<TResult>(query, selector, includes, asTracking);
        });
    }

    public T GetSingle(Expression<Func<T, bool>>? query, 
        List<string>? includes = null,
        bool asTracking = false)
    {
        string key = GenerateCacheKey(query, selector: null, orderBy: null, orderType: null, includes, skip: null,
            take: null, distinct: null);
        return _memoryCache.GetOrCreate<T>(key, (entry) =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _repository.GetSingle(query, includes, asTracking);
        });
    }

    #endregion

    #region withOutCache
    public Task<IQueryable<T>> GetQueriableAsync()
        => _repository.GetQueriableAsync();
    public IQueryable<T> GetQueriable()
        => _repository.GetQueriable();
    public bool Any(Expression<Func<T, bool>> query)
        =>  _repository.Any(query);

    public long GetCount(Expression<Func<T, bool>>? query = default)
        => _repository.GetCount(query);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> query)
        => await _repository.AnyAsync(query);

    #endregion
    
    #region Helper

    private string GenerateCacheKey(object? query = null, object selector = null, object? orderBy = null,
        OrderType? orderType = null, List<string>? includes = null, int? skip = null, int? take = null,
        bool? distinct = null)
    {
        string collectionName = typeof(T).Name;
        string key = collectionName;
        if (query is not null) key += query.ToString()?.GetHashCode().ToString();
        if (selector is not null) key += selector.ToString()?.GetHashCode().ToString();
        if (orderBy is not null) key += orderBy.ToString()?.GetHashCode().ToString();
        if (orderType is not null) key += orderType.GetHashCode().ToString();
        if (skip is not null) key += skip.GetHashCode().ToString();
        if (take is not null) key += take.GetHashCode().ToString();
        if (distinct is not null) key += distinct.GetHashCode().ToString();
        return key;
    }

    #endregion
}