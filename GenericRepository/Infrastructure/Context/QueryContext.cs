using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Infrastructure.Context;

public class QueryContext : DbContext
{
    public QueryContext(DbContextOptions<QueryContext> options) : base(options)
    {
        
    }
    
}