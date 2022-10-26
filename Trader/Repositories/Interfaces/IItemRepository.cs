using Trader.Api.DataAcess;
using Trader.Api.Domain.Models;

namespace Trader.Api.Repositories.Interfaces
{
    public interface IItemRepository: IBaseRepository<Item>
    {
        Task<IEnumerable<Item>> GetByPersonId(int PersonId);
    }
}
