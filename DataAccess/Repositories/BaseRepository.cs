using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SkillManagementContext _context;

        public BaseRepository(SkillManagementContext Context)
        {
            _context = Context;
        }

        public IList<T> GetAll()
        {
            var set = _context.Set<T>();
            return set.AsNoTracking().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
    }
}