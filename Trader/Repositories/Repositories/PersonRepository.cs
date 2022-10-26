using Microsoft.EntityFrameworkCore;
using Trader.Api.DataAcess;
using Trader.Api.Domain.Models;
using Trader.Api.Repositories.Interfaces;

namespace Trader.Api.Repositories.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApiDbContext context) : base(context)
        {
        }

        public override async Task<Person> Get(int id)
        {
            return await _context.Person
                .Include(p => p.Items)
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
