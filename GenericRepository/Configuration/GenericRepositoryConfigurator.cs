using GenericRepository.Abstractions.Interfaces.GenericRepository.Command;
using GenericRepository.Abstractions.Interfaces.GenericRepository.Query;
using GenericRepository.Abstractions.Interfaces.UnitOfWork;
using GenericRepository.Data;
using GenericRepository.Repository.GenericRepository.Command;
using GenericRepository.Repository.GenericRepository.Query;
using GenericRepository.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GenericRepository.Configuration
{
  public static class GenericRepositoryConfigurator
  {
    public static void InjectServices(IServiceCollection services)
    {
      services.AddMemoryCache();
      services.AddTransient<IUnitOfwork, UnitOfWork>();
      services.AddScoped<DbContext, CommandContext>();
      services.AddScoped<DbContext, QueryContext>();
      services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
      services.AddTransient(typeof(IQueryGenericRepository<>), typeof(QueryGenericRepository<>));
      services.Decorate(typeof(IQueryGenericRepository<>), typeof(CacheRepository<>));
    }
  }
}
