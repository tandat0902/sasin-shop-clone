namespace api_sasin.Models
{
    public class Products
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Discount { get; set; }   
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool HomeFlag { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int UnitsInStock { get; set; }
        public string CategoryId { get; set; }
    }
}
