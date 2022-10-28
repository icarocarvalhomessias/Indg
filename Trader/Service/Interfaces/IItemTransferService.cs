using Trader.Api.Domain.Models;

namespace Trader.Api.Service.Interfaces
{
    public interface IItemTransferService
    {
        Task<IEnumerable<ItemTransfer>> Get();

        Task Transfer(int FromPersonId, int ToPersonId, int ItemId);
    }
}
