using AutoMapper;
using NewsAggregator.BLL.DTO;
using NewsAggregator.BLL.Interfaces;
using NewsAggregator.BLL.Models;

namespace NewsAggregator.BLL
{
    public class RssNewsUpdater : IRssNewsUpdater
    {
        const int READING_DELAY = 60_000;

        IRssNewsReader _rssNewsReader;
        NewsModelList _newsModelList;
        bool _isLinkCorrect = false;
        INewsService _newsService;
        UsedWebSites _usedWebSites;

        public bool IsLinkCorrect => _isLinkCorrect;

        public RssNewsUpdater(INewsService newsService, IRssNewsReader rssNewsReader)
        {
            _newsService = newsService;
            _rssNewsReader = rssNewsReader;
        }

        public void Init(string link)
        {
            _newsModelList = new NewsModelList();
            _rssNewsReader.Init(link, _newsModelList);
            GetNewsSourceId(link);
        }

        public async Task UpdateNewsAsync()
        {
            while (true)
            {
                try
                {
                    _rssNewsReader.GetNews();
                    _isLinkCorrect = true;
                }
                catch (Exception)
                {
                    _isLinkCorrect = false;
                }

                if (_newsModelList.IsNewDataReceived)
                {
                    SaveNewNews();
                }

                await Task.Delay(READING_DELAY);
            }
        }

        #region Private Methods
        private void SaveNewNews()
        {
            var mapper = new MapperConfiguration(m => m.CreateMap<NewsModel, NewsDTO>()).CreateMapper();
            NewsDTO newsDTO;

            foreach (var item in _newsModelList.ModelsList)
            {
                newsDTO = mapper.Map<NewsModel, NewsDTO>(item);
                newsDTO.NewsSourcesId = (int)_usedWebSites;
                _newsService.SaveNews(newsDTO);
            }
        }

        private void GetNewsSourceId(string link)
        {
            if (link.Contains("tass"))
            {
                _usedWebSites = UsedWebSites.Tass;
            }
            if (link.Contains("lenta"))
            {
                _usedWebSites = UsedWebSites.Lenta;
            }
        }

        enum UsedWebSites
        {
            Tass = 1,
            Lenta
        }
        #endregion
    }
}
