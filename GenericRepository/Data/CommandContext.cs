using GenericRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenericRepository.Data;

public class CommandContext : DbContext
{
    public CommandContext(DbContextOptions<CommandContext> options) : base(options)
    {
        ChangeTracker.StateChanged += TrackChanges;
        ChangeTracker.Tracked += TrackChanges;
    }
    private static void TrackChanges(object sender, EntityEntryEventArgs e)
    {
        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        if (e.Entry.Entity is BaseModel model)
        {
            var result = e.Entry.State switch
            {
                    EntityState.Deleted => model.DeleteDate = now,
                    EntityState.Modified => model.UpdateDate = now,
                    EntityState.Added => model.CreateDate = now,
                    _ => default,
            };
        }
    }
}