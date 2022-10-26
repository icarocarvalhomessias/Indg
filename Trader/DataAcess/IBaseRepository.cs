namespace Trader.Api.DataAcess
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T?> Get(int id);
        Task Save();

        Task Insert(T entity);
    }
}
