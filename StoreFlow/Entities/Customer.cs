namespace StoreFlow.Entities
{
    public class Customer //Müşteri
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerCity { get; set; }
        public string? CustomerDistrict { get; set; } //İlçe zorunlu değil
        public decimal CustomerBalance { get; set; } //Bakiye
        public string? CustomerImageUrl { get; set; }
        public List<Order> Orders { get; set; }
    }
}
