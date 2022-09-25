using Api2.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api2.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext context;

        public EventRepository(EventContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Launch stored sql procedure with passed data
        /// </summary>
        /// <param name="s_items"></param>
        /// <returns></returns>
        public async Task AddEvents(string s_items)
        {
            if (!string.IsNullOrWhiteSpace(s_items))
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "s_items";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = s_items;
                await Task.Run(async () => await context.Database.ExecuteSqlRawAsync("EXEC sp_event_odd_do @s_items={0}", parameter));
            }
        }
    }
}
