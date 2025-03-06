namespace PomeloSoft.Dtos
{
	public class ShopifyOrderDto
	{
		public string Id { get; set; }
		public ShopifyCustomerDto Customer { get; set; }
		public List<ShopifyProductDto> LineItems { get; set; }
		public string FinancialStatus { get; set; } // "paid", "pending", "refunded"
		public DateTime CreatedAt { get; set; }
	}

	public class ShopifyCustomerDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}

	public class ShopifyProductDto
	{
		public string ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
