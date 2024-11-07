using GenericRepository.Application.Interfaces.GenericRepository.Command;
using GenericRepository.Application.Interfaces.GenericRepository.Query;
using GenericRepository.Application.Interfaces.UnitOfWork;
using GenericRepository.Infrastructure.Context;
using GenericRepository.Infrastructure.EfInterceptors;
using GenericRepository.Infrastructure.Jobs;
using GenericRepository.Infrastructure.Repository.GenericRepository.Command;
using GenericRepository.Infrastructure.Repository.GenericRepository.Query;
using GenericRepository.Infrastructure.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GenericRepository.Infrastructure.Ioc
{
  public static class GenericRepositoryConfigurator
  {
    public static void InjectServices(IServiceCollection services)
    {
      services.AddMemoryCache();
      services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
      services.AddScoped<DbContext, CommandContext>();
      services.AddScoped<DbContext, QueryContext>();
      services.AddSingleton<PublishDomainEventsInterceptor>();
      services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
      services.AddTransient(typeof(IQueryGenericRepository<>), typeof(QueryGenericRepository<>));
      services.Decorate(typeof(IQueryGenericRepository<>), typeof(CacheRepository<>));
      services.AddHostedService<OutboxProcessorJob>();
    }
  }
}
