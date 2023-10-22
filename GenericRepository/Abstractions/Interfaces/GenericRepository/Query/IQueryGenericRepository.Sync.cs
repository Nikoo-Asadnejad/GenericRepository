using System.Linq.Expressions;
using GenericRepository.Enumerations;

namespace GenericRepository.Abstractions.Interfaces.GenericRepository.Query;

public partial interface IQueryGenericRepository<T>
{
    IQueryable<T> GetQueryable();
    T Find(long id);

    TResult GetSingle<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        List<string>? includes = default,
        bool asTracking = false);

    T GetSingle(Expression<Func<T, bool>>? query,
        List<string>? includes = null,
        bool asTracking = false);

    IReadOnlyList<TResult> GetList<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = default,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);


    IReadOnlyList<T> GetList(Expression<Func<T, bool>>? query = default,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = 0,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);

    IReadOnlyList<TResult> GetAll<TResult>(Func<T, TResult> selector,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = null,
        int? take = null,
        bool? distinct = null,
        bool asTracking = false);

    IReadOnlyList<T> GetAll(Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = null,
        int? skip = 0,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);
    
    long GetCount(Expression<Func<T, bool>>? query = default);
    bool Any(Expression<Func<T, bool>> query);
}