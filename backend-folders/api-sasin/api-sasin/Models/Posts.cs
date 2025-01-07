namespace api_sasin.Models
{
    public class Posts
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ShortContents { get; set; }
        public string Contents { get; set; }
        public string Thumnail { get; set; }
        public bool Pushlished { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
        public bool isHot { get; set; }
        public bool IsNewFeed { get; set; }
        public int Views {  get; set; }
    }
}
