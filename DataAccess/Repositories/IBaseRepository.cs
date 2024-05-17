namespace DataAccess.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IList<T> GetAll();

        void Add(T entity);
    }
}
