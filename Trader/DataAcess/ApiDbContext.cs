using Microsoft.EntityFrameworkCore;

using Trader.Api.Domain.Models;

namespace Trader.Api.DataAcess
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) 
        {

        }

        public DbSet<Person> Person { get; set; }

        public DbSet<Trader.Api.Domain.Models.Item> Item { get; set; }

        public DbSet<Trader.Api.Domain.Models.ItemTransfer> ItemTransfer{ get; set; }
    }
}
