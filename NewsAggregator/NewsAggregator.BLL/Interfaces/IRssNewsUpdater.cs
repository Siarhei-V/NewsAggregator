namespace NewsAggregator.BLL.Interfaces
{
    public interface IRssNewsUpdater
    {
        public bool IsLinkCorrect { get; }

        public void Init(string link);
        Task UpdateNewsAsync();
    }
}
