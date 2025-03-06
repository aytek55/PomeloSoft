namespace PomeloSoft.Models
{
	public class Order
	{
		public int Id { get; set; } // Primary Key
		public string OrderNumber { get; set; } = Guid.NewGuid().ToString(); // Benzersiz sipariş numarası
		public Customer Customer { get; set; } // Müşteri bilgileri
		public List<OrderItem> Items { get; set; } = new(); // Sipariş ürünleri
		public OrderStatus Status { get; set; } = OrderStatus.Pending; // Sipariş durumu
		public Marketplace Marketplace { get; set; } // Siparişin geldiği platform
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Sipariş tarihi
	}

}
