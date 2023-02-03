using NewsAggregator.DAL.EF;
using NewsAggregator.DAL.Entities;
using NewsAggregator.DAL.Interfaces;

namespace NewsAggregator.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationContext _db;
        readonly IRepository<News> _newsRepository;
        readonly IRepository<NewsSources> _newsSourcesRepository;

        public UnitOfWork()
        {
            _db = new ApplicationContext();
        }

        public IRepository<NewsSources> NewsSources => _newsSourcesRepository ?? new EFNewsSourcesRepository(_db);

        public IRepository<News> News => _newsRepository ?? new EFNewsRepository(_db);

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
