using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Data;

public class QueryContext : DbContext
{
    public QueryContext(DbContextOptions<QueryContext> options) : base(options)
    {
        
    }
    
}