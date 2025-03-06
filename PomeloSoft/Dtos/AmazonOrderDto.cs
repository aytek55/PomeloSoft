namespace PomeloSoft.Dtos
{
	public class AmazonOrderDto
	{
		public string OrderId { get; set; }
		public AmazonBuyerDto Buyer { get; set; }
		public List<AmazonItemDto> Items { get; set; }
		public string OrderStatus { get; set; } // "Pending", "Shipped", "Delivered", "Cancelled"
		public DateTime PurchaseDate { get; set; }
	}

	public class AmazonBuyerDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
	}

	public class AmazonItemDto
	{
		public string Asin { get; set; }
		public string Description { get; set; }
		public decimal Cost { get; set; }
		public int Count { get; set; }
	}

}
