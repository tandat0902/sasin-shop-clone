namespace api_sasin.Models
{
    public class Pages
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string Contents { get; set; }
        public string Thumnail { get; set; }
        public bool Published { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public int Ordering {  get; set; }
    }
}
