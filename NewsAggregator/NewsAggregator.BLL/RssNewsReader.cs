using NewsAggregator.BLL.Interfaces;
using NewsAggregator.BLL.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsAggregator.BLL
{
    public class RssNewsReader : IRssNewsReader
    {
        string _link;
        NewsModelList _newsModelList;
        NewsModel _newsModel;
        bool _isFirstStart = true;

        public void Init(string link, NewsModelList newsModelList)
        {
            _link = link;
            _newsModelList = newsModelList;
        }

        public void GetNews()
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(_link))
                {
                    var formatter = new Rss20FeedFormatter();
                    formatter.ReadFrom(reader);

                    if (_isFirstStart)
                    {
                        AddAllNews(formatter);
                    }
                    else
                    {
                        AddNewNews(formatter);
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private void AddAllNews(Rss20FeedFormatter formatter)
        {
            foreach (var item in formatter.Feed.Items)
            {
                _newsModel = new NewsModel
                {
                    Guid = item.Id,
                    Title = item.Title.Text,
                    Link = item.Links.FirstOrDefault().Uri.ToString()
                };

                _newsModelList.ModelsList.Add(_newsModel);
            }
            _newsModelList.IsNewDataReceived = true;
            _isFirstStart = false;
        }

        private void AddNewNews(Rss20FeedFormatter formatter)
        {
            if (_newsModelList.ModelsList.FirstOrDefault().Guid == formatter.Feed.Items.FirstOrDefault().Id)
            {
                _newsModelList.IsNewDataReceived = false;
                return;
            }

            _newsModelList.ModelsList.RemoveRange(1, _newsModelList.ModelsList.Count - 1);

            foreach (var item in formatter.Feed.Items)
            {
                _newsModel = new NewsModel
                {
                    Guid = item.Id,
                    Title = item.Title.Text,
                    Link = item.Links.FirstOrDefault().Uri.ToString()
                };


                if (_newsModelList.ModelsList.FirstOrDefault().Guid != _newsModel.Guid)
                {
                    _newsModelList.ModelsList.Add(_newsModel);
                }
                else
                {
                    _newsModelList.IsNewDataReceived = true;
                    _newsModelList.ModelsList.RemoveAt(0);
                    return;
                }
            }
        }
        #endregion
    }
}
