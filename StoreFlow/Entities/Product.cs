namespace StoreFlow.Entities
{
    public class Product //Ürün
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
    }
}
