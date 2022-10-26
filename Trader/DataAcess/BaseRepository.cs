using Microsoft.EntityFrameworkCore;

namespace Trader.Api.DataAcess
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApiDbContext _context;

        public BaseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
