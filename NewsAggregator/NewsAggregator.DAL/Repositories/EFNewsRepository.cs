using Microsoft.EntityFrameworkCore;
using NewsAggregator.DAL.EF;
using NewsAggregator.DAL.Entities;
using NewsAggregator.DAL.Interfaces;

namespace NewsAggregator.DAL.Repositories
{
    public class EFNewsRepository : IRepository<News>
    {
        readonly ApplicationContext _db;

        public EFNewsRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public void Create(News entity)
        {
            _db.News.Add(entity);
        }

        public List<News> Find(string str)
        {
            IQueryable<News> news = _db.News;
            return news.Where(n => n.Title.Contains(str)).ToList();
        }

        public List<News> GetAll(string link)
        {
            var newsSource = _db.NewsSources.FirstOrDefault(s => s.Name == link);

            if (newsSource == null)
            {
                throw new Exception("Источник новостей не найден");
            }

            _db.News.Where(n => n.NewsSourcesId == newsSource.Id).Load();

            return newsSource.NewsList;
        }
    }
}
