namespace StoreFlow.Entities
{
    public class Order //Sipariş
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int OrderCount { get; set; } //Sipariş Adedi
        public decimal UnitPrice { get; set; } //Birim Fiyat
        public decimal TotalPrice { get; set; } //Toplam Fiyat
        public DateTime OrderDate { get; set; } //Sipariş Tarihi
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        public string? Status { get; set; }

    }
}
