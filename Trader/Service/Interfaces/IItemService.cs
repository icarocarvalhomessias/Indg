using Trader.Api.Domain.Models;

namespace Trader.Api.Service.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> Get();
        Task<Item> Get(int id);
        Task Insert(string name);
        Task Update(Item item);
        Task Delete(int id);
        Task InativateItens(int PersonId);
    }
}
