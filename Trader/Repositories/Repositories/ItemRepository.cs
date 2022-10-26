using Microsoft.EntityFrameworkCore;

using Trader.Api.DataAcess;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;

namespace Trader.Api.Repositories.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Item>> GetByPersonId(int PersonId)
        {
            return await _context.Item.Where(x => x.PersonId == PersonId).ToListAsync();
        }
    }
}
