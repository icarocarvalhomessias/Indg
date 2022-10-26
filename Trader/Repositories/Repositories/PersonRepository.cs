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
    }
}
