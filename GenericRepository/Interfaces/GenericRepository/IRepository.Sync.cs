using GenericReositoryDll.Enumrations;
using System.Linq.Expressions;

namespace GenericRepository.Interfaces.Repository;

public partial interface IRepository<T>
{
    
    IQueryable<T> GetQueriable();
    T Find(long id);

    TResult GetSingle<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        List<string>? includes = default,
        bool asTracking = false);

    T GetSingle(Expression<Func<T, bool>>? query,
        List<string>? includes = null,
        bool asTracking = false);

    List<TResult> GetList<TResult>(Expression<Func<T, bool>>? query,
        Func<T, TResult> selector,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = default,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);


    List<T> GetList(Expression<Func<T, bool>>? query = default,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = 0,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);

    List<TResult> GetAll<TResult>(Func<T, TResult> selector,
        Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = default,
        int? skip = null,
        int? take = null,
        bool? distinct = null,
        bool asTracking = false);

    List<T> GetAll(Func<T, object>? orderBy = default,
        OrderType? orderType = OrderType.Asc,
        List<string>? includes = null,
        int? skip = 0,
        int? take = null,
        bool? distinct = false,
        bool asTracking = false);
    
    long GetCount(Expression<Func<T, bool>>? query = default);
    void Add(T model);
    void AddRange(IEnumerable<T> models);
    void Update(T model);
    void UpdateRange(IEnumerable<T> models);
    void Delete(long id);
    void Delete(T model);
    void DeleteRange(IEnumerable<T> models);
    bool Any(Expression<Func<T, bool>> query);

}