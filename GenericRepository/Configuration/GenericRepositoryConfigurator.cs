using GenericRepository.Interfaces.Repository;
using GenericRepository.Interfaces.UnitOfWork;
using GenericRepository.Models;
using GenericRepository.Repository;
using GenericRepository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GenericRepositoryDll.Configuration
{
  public static class GenericRepositoryConfigurator
  {
    public static void InjectServices(IServiceCollection services)
    {
      services.AddMemoryCache();
      services.AddTransient<IUnitOfwork, UnitOfWork>();
      services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
      services.AddTransient(typeof(IQueryGenericRepository<>), typeof(QueryGenericRepository<>));
      services.Decorate(typeof(IQueryGenericRepository<>), typeof(CacheRepository<>));
    }
  }
}
