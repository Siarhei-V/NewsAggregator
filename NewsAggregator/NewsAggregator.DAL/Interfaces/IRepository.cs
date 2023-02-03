namespace NewsAggregator.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        List<T> GetAll(string link);
        List<T> Find(string str);
    }
}
