using NewsAggregator.DAL.Entities;

namespace NewsAggregator.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<NewsSources> NewsSources { get; }
        IRepository<News> News { get; }
        void Save();
    }
}
