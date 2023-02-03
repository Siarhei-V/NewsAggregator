namespace NewsAggregator.BLL.Models
{
    public class NewsModel
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }

    public class NewsModelList
    {
        public List<NewsModel> ModelsList { get; set; } = new List<NewsModel>();
        public bool IsNewDataReceived { get; set; }
    }
}
