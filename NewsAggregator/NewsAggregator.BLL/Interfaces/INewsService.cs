using NewsAggregator.BLL.DTO;

namespace NewsAggregator.BLL.Interfaces
{
    public interface INewsService
    {
        List<NewsDTO> GetNews(string link);
        public List<NewsDTO> FindNews(string str);
        void SaveNews(NewsDTO newsDTO);
    }
}
