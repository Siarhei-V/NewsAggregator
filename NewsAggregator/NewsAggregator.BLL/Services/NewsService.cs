using AutoMapper;
using NewsAggregator.BLL.DTO;
using NewsAggregator.BLL.Interfaces;
using NewsAggregator.DAL.Entities;
using NewsAggregator.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NewsAggregator.BLL.Services
{
    public class NewsService : INewsService
    {
        readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<NewsDTO> GetNews(string link)
        {
            var news = _unitOfWork.News.GetAll(link);

            if (news == null)
            {
                throw new ArgumentException("Новости не найдены");
            }

            var mapper = new MapperConfiguration(m => m.CreateMap<News, NewsDTO>()).CreateMapper();
            var newsDTO = mapper.Map<List<News>, List<NewsDTO>>(news);

            return newsDTO;
        }

        public List<NewsDTO> FindNews(string str)
        {
            var result = _unitOfWork.News.Find(str);

            if (result == null)
            {
                throw new ArgumentException("Новости не найдены");
            }

            var mapper = new MapperConfiguration(m => m.CreateMap<News, NewsDTO>()).CreateMapper();
            return mapper.Map<List<News>, List<NewsDTO>>(result);
        }

        public void SaveNews(NewsDTO newsDTO)
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<NewsDTO, News>()).CreateMapper();
            var news = mapper.Map<NewsDTO, News>(newsDTO);
            _unitOfWork.News.Create(news);
            _unitOfWork.Save();
        }
    }
}
