
using GenericReositoryDll.Enumrations;
using System.Linq.Expressions;
using GenericRepository.Models;

namespace GenericRepository.Interfaces.Repository;


public partial interface IRepository<T> where T : BaseModel
{

  Task<T> FindAsync(long id);

    Task<TResult> GetSingleAsync<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        List<string>? includes = default,
        bool asTracking = false);

    Task<T> GetSingleAsync(Expression<Func<T, bool>>? query,
        List<string>? includes = default,
        bool asTracking = false);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query">Query </param>
    /// <param name="selector">Select </param>
    /// <param name="orderBy">Order By</param>
    /// <param name="orderType">Asc| Desc</param>
    /// <param name="includes">include</param>
    /// <param name="skip">skip</param>
    /// <param name="take">take</param>
    /// <param name="distinct">isDistinct</param>
    /// <param name="asTracking">On or Off tracking</param>
    /// <returns></returns>
    Task<List<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = null,
        int? take = null,
        bool? distinct = null,
        bool asTracking = false);


    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? query = default,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = default,
        int? take = default,
        bool? distinct = default,
        bool asTracking = false);


    Task<List<TResult>> GetAllAsync<TResult>(Func<T, TResult> selector,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = 0,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);

    Task<List<T>> GetAllAsync(Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = 0,
        int? take = null,
        bool? distinct = null,
        bool asTracking = false);


    Task<long> GetCountAsync(Expression<Func<T, bool>>? query = default);

    Task AddAsync(T model);

    Task AddRangeAsync(IEnumerable<T> models);

    Task UpdateAsync(T model);

    Task UpdateRangeAsync(IEnumerable<T> models);

    Task DeleteAsync(long id);
    
    Task DeleteAsync(T model);

    Task DeleteRangeAsync(IEnumerable<T> models);

    Task<bool> AnyAsync(Expression<Func<T, bool>> query);
    
}

