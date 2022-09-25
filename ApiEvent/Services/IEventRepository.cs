namespace Api2.Services
{
    public interface IEventRepository
    {
        Task AddEvents(string events);
    }
}
