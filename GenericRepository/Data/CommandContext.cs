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
        if (e.Entry.Entity is BaseEntity model)
        {
            var result = e.Entry.State switch
            {
                    EntityState.Deleted => model.Delete(),
                    EntityState.Modified => model.Update(),
                    EntityState.Added => model.Create(),
                    _ => default,
            };
        }
    }
}