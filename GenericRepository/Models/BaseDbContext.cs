using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenericRepository.Models;

public class BaseDbContext : DbContext
{
    public BaseDbContext()
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