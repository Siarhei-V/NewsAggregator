namespace NewsAggregator.DAL.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int NewsSourcesId { get; set; }
        public NewsSources NewsSource { get; set; }
    }
}
