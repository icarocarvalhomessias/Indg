using Trader.Api.Domain.Models;

namespace Trader.Api.Service.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> Get();
        Task<Person> Get(int id);
        Task Insert(string name);
        Task Update(int PersonId, string NewName);
        Task Delete(int id);

    }
}
