using Trader.Api.DataAcess;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;

namespace Trader.Api.Repositories.Repositories
{
    public class ItemTransferRepository : BaseRepository<ItemTransfer>, IItemTransferRepository
    {
        public ItemTransferRepository(ApiDbContext context) : base(context)
        {
        }
    }
}
