using NewsAggregator.DAL.EF;
using NewsAggregator.DAL.Entities;
using NewsAggregator.DAL.Interfaces;

namespace NewsAggregator.DAL.Repositories
{
    public class EFNewsSourcesRepository : IRepository<NewsSources>
    {
        readonly ApplicationContext _db;

        public EFNewsSourcesRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public void Create(NewsSources entity)  // TODO: implement methods
        {
            throw new NotImplementedException();
        }

        public List<NewsSources> Find(string str)
        {
            throw new NotImplementedException();
        }

        public List<NewsSources> GetAll(string link)
        {
            throw new NotImplementedException();
        }
    }
}
