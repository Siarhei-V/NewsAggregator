namespace NewsAggregator.DAL.Entities
{
    public class NewsSources
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<News> NewsList { get; set; } = new List<News>();
    }
}
