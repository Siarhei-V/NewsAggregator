using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsAggregator.API.Models;
using NewsAggregator.BLL.DTO;
using NewsAggregator.BLL.Interfaces;

namespace NewsAggregator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NewsController : ControllerBase
    {
        readonly INewsService _newsService;
        readonly IRssNewsUpdater _newsUpdater;

        public NewsController(INewsService newsService, IRssNewsUpdater rssNewsUpdater)
        {
            _newsService = newsService;
            _newsUpdater = rssNewsUpdater;
        }

        [HttpGet]
        public List<NewsApiModel> GetAll(string link)
        {
            var news = _newsService.GetNews(link);
            var mapper = new MapperConfiguration(m => m.CreateMap<NewsDTO, NewsApiModel>()).CreateMapper();
            var result = mapper.Map<List<NewsDTO>, List<NewsApiModel>>(news);

            return result;
        }

        [HttpGet]
        public List<string> Find(string str)
        {
            var news = _newsService.FindNews(str);
            var result = new List<string>();

            foreach (var item in news)
            {
                result.Add(item.Title);
            }

            return result;
        }

        [HttpGet]
        public string Start(string link)
        {
            _newsUpdater.Init(link);

            _ = _newsUpdater.UpdateNewsAsync();

            if (_newsUpdater.IsLinkCorrect)
            {
                return "Начинаю загружать новости";
            }
            else
            {
                return "Я не умею работать с такой ссылкой";
            }
        }
    }
}