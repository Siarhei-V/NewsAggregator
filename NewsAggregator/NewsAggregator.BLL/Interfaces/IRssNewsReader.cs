using NewsAggregator.BLL.Models;

namespace NewsAggregator.BLL.Interfaces
{
    public interface IRssNewsReader
    {
        public void Init(string link, NewsModelList newsModelList);
        void GetNews();
    }
}
