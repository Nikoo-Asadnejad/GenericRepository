using GenericRepository.Application.Interfaces.GenericRepository.Command;
using GenericRepository.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork<TContext> where TContext : DbContext
{ 
    IRepository<OutBoxMessage> OutboxMessages { get; }
    Task<int> SaveAsync();
    int Save();
    Task RollbackTransactionAsync();
    Task CommitTransactionAsync();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    IDbContextTransaction BeginTransaction();
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    void RollbackTransaction();
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    void CommitTransaction();
    Task<int> ExecuteSqlCommandAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);
    int ExecuteSqlCommand(string sql, params object[] parameters);
    Task<List<T>> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken = default) where T : class;
    List<T> ExecuteSqlQuery<T>(string sql, params object[] parameters) where T : class;
    Task ExecuteSqlWithinTransactionAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);
    void ExecuteSqlWithinTransaction(string sql, params object[] parameters);
    Task<List<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object[] parameters, CancellationToken cancellationToken = default) where T : class;
    List<T> ExecuteStoredProcedure<T>(string storedProcedureName, params object[] parameters) where T : class;
    Task<int> ExecuteSqlCommandWithParamsAsync(string sql, SqlParameter[] parameters, CancellationToken cancellationToken = default);
    int ExecuteSqlCommandWithParams(string sql, params SqlParameter[] parameters);
    bool IsRawSqlSupported();
}