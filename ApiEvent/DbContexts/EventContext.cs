using Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Api2.DbContexts
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<Odds> Odds { get; set; }
        public EventContext(DbContextOptions<EventContext> options)
            :base(options)
        {

        }
    }
}
