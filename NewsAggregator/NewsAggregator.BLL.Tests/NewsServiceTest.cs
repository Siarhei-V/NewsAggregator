using Moq;
using NewsAggregator.BLL.DTO;
using NewsAggregator.BLL.Interfaces;
using NewsAggregator.BLL.Services;
using NewsAggregator.DAL.Entities;
using NewsAggregator.DAL.Interfaces;
using Xunit;

namespace NewsAggregator.BLL.Tests
{
    public class NewsServiceTest
    {
        Mock<IUnitOfWork> _unitOfWorkMock;
        INewsService _newsService;
        List<News> _newsList;

        public NewsServiceTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _newsService = new NewsService(_unitOfWorkMock.Object);

            _newsList = new List<News>()
            {
                new News() { Id = 1, Title = "The first article",   Link = "firstlink.ru",  NewsSourcesId = 1 },
                new News() { Id = 2, Title = "The second article",  Link = "secondlink.ru", NewsSourcesId = 1 },
                new News() { Id = 3, Title = "The third article",   Link = "thirdlink.ru",  NewsSourcesId = 2 },
                new News() { Id = 4, Title = "The fourth article",  Link = "fourthlink.ru", NewsSourcesId = 2 },
            };
        }

        #region GetNews Tests
        [Fact]
        public void GetNews_CheckNullResult()
        {
            // Arrange
            List<News>? news = null;
            _unitOfWorkMock.Setup(m => m.News.GetAll(It.IsAny<string>())).Returns(news!);

            // Act, Assert
            try
            {
                var res = _newsService.GetNews("");
            }
            catch (Exception ex)
            {
                Assert.Equal("Новости не найдены", ex.Message);
            }
        }

        [Fact]
        public void GetNews_CheckReturnedValue()
        {
            // Arrange
            string link = "firstlink.ru";
            _unitOfWorkMock.Setup(m => m.News.GetAll(link)).Returns(_newsList.Where(n => n.Link == link).ToList());

            // Act
            var res = _newsService.GetNews(link);

            //Assert
            List<NewsDTO> newsDtoList = new List<NewsDTO>()
            {
                new NewsDTO() { Title = "The first article", Link = "firstlink.ru" },
            };

            Assert.Equal(newsDtoList.Count, res.Count);
            for (int i = 0; i < res.Count; i++)
            {
                Assert.Equal(newsDtoList[i].Title, res[i].Title);
                Assert.Equal(newsDtoList[i].Link, res[i].Link);
            }
        }
        #endregion

        #region FindNews Tests
        [Fact]
        public void FindNews_CheckNullResult()
        {
            // Arrange
            List<News>? news = null;
            _unitOfWorkMock.Setup(m => m.News.Find(It.IsAny<string>())).Returns(news!);

            // Act, Assert
            try
            {
                var res = _newsService.FindNews("");
            }
            catch (Exception ex)
            {
                Assert.Equal("Новости не найдены", ex.Message);
            }
        }

        [Fact]
        public void FindNews_CheckReturnedValue()
        {
            // Arrange
            string str = "second";
            _unitOfWorkMock.Setup(m => m.News.Find(str)).Returns(_newsList.Where(n => n.Title.Contains(str)).ToList());

            // Act
            var res = _newsService.FindNews(str);

            //Assert
            List<NewsDTO> newsDtoList = new List<NewsDTO>()
            {
                new NewsDTO() { Title = "The second article", Link = "secondlink.ru" },
            };

            Assert.Equal(newsDtoList.Count, res.Count);
            for (int i = 0; i < res.Count; i++)
            {
                Assert.Equal(newsDtoList[i].Title, res[i].Title);
                Assert.Equal(newsDtoList[i].Link, res[i].Link);
            }
        }
        #endregion
    }
}