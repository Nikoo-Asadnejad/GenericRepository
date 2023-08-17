namespace GenericRepository.Abstractions.Interfaces.UnitOfWork;

public interface IUnitOfwork
{ 
    Task<int> SaveAsync();
    int Save();
    Task DisposeAsync();
    void Dispose();
}