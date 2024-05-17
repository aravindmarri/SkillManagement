using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IList<T> GetAll();

        T FindBy(Expression<Func<T, bool>> predicate);

        void Add(T entity);
    }
}
