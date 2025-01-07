namespace api_sasin.Models
{
    public class OrderDetails
    {
        public string OrderDetailId { get; set; }
        public string OrderId {  get; set; }
        public string ProductId { get; set; }
        public int OrderNumber { get; set; }
        public int Quatity { get; set; } 
        public int CodeId { get; set; }
        public decimal Total { get; set; }
        public DateTime ShipDate { get; set; }
    }
}
