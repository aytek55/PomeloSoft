using PomeloSoft.Models;

namespace PomeloSoft.Dtos
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public Customer Customer { get; set; }
		public decimal TotalPrice { get; set; }
		public OrderStatus Status { get; set; }
		public Marketplace Marketplace { get; set; }
	}

}
