namespace api_sasin.Models
{
    public class Customers
    {
        public string CustomerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string City { get; set; }
        public string Salt { get; set; }
        public bool Action { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
