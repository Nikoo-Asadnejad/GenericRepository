using GenericRepository.Application.Interfaces.UnitOfWork;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericRepository.Infrastructure.Repository.UnitOfWork;

public class UnitOfWork<TContext> :  IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _context;
    public UnitOfWork(TContext context)
    {
        _context = context;
    }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public int Save()
    {
        return _context.SaveChanges();
    }
    
    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }
    
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.RollbackTransactionAsync(cancellationToken);
    }
    
    public void RollbackTransaction()
    {
        _context.Database.RollbackTransaction();
    }
    
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public void CommitTransaction()
    {
        _context.Database.CommitTransaction();
    }
    
    public async Task<int> ExecuteSqlCommandAsync(string sql, object[] parameters, CancellationToken cancellationToken = default)
    {
        return await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }
    
    public int ExecuteSqlCommand(string sql, params object[] parameters)
    {
        return _context.Database.ExecuteSqlRaw(sql, parameters);
    }
    
    public async Task<List<T>> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken = default) where T : class
    {
        return await _context.Set<T>().FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
    }
    
    public List<T> ExecuteSqlQuery<T>(string sql, params object[] parameters) where T : class
    {
        return _context.Set<T>().FromSqlRaw(sql, parameters).ToList();
    }

    public async Task ExecuteSqlWithinTransactionAsync(string sql, object[] parameters, CancellationToken cancellationToken = default)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
    
    public void ExecuteSqlWithinTransaction(string sql, params object[] parameters)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Database.ExecuteSqlRaw(sql, parameters);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
    
    public async Task<List<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, object[] parameters, CancellationToken cancellationToken = default) where T : class
    {
        return await _context.Set<T>().FromSqlRaw($"EXEC {storedProcedureName}", parameters).ToListAsync(cancellationToken);
    }
    
    public List<T> ExecuteStoredProcedure<T>(string storedProcedureName, params object[] parameters) where T : class
    {
        return _context.Set<T>().FromSqlRaw($"EXEC {storedProcedureName}", parameters).ToList();
    }
    
    public async Task<int> ExecuteSqlCommandWithParamsAsync(string sql, SqlParameter[] parameters, CancellationToken cancellationToken = default)
    {
        return await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }
    
    public int ExecuteSqlCommandWithParams(string sql, params SqlParameter[] parameters)
    {
        return _context.Database.ExecuteSqlRaw(sql, parameters);
    }
    
    public bool IsRawSqlSupported()
    {
        return _context.Database.CanConnect();
    }
}