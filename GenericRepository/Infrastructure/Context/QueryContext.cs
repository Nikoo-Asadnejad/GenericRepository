using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Context;

public class QueryContext : DbContext
{
    public QueryContext(DbContextOptions<QueryContext> options) : base(options)
    {
        
    }
    
}