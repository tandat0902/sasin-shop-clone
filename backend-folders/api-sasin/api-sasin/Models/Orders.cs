namespace api_sasin.Models
{
    public class Orders
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TransactStatusId { get; set; }
        public bool Deleted { get; set; }   
        public bool Paid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Note { get; set; }
    }
}
