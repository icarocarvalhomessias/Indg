using Trader.Api.Domain.Models;

namespace Trader.Api.Service.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> Get();
        Task<Item> Get(int id);
        Task Insert(string ItemName, int PersonId);
        Task ChangeOwner(Item item);
        Task Update(int itenId, string NewItemName);
        Task Delete(int id);
        Task InativateItens(int PersonId);
    }
}
