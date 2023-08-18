using GenericRepository.Abstractions.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfwork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync()
    {
        int result = await _context.SaveChangesAsync();
        return result;
    }
    public int Save()
        => _context.SaveChanges();
    public async Task DisposeAsync()
        => await _context.DisposeAsync();
    public void Dispose()
        => _context.Dispose();



}