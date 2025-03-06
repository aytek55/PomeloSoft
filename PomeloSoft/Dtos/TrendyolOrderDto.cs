namespace PomeloSoft.Dtos
{
	public class TrendyolOrderDto
	{
		public long OrderNumber { get; set; }
		public TrendyolCustomerDto Buyer { get; set; }
		public List<TrendyolProductDto> Products { get; set; }
		public string Status { get; set; } // "Awaiting", "Shipped", "Completed"
		public string OrderDate { get; set; } // "2025-03-06T12:00:00"
	}

	public class TrendyolCustomerDto
	{
		public string FullName { get; set; }
		public string ContactEmail { get; set; }
	}

	public class TrendyolProductDto
	{
		public string Sku { get; set; }
		public string Title { get; set; }
		public double SalePrice { get; set; }
		public int Amount { get; set; }
	}

}
