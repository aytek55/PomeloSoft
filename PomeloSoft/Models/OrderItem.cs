namespace PomeloSoft.Models
{
	public class OrderItem
	{
		public int Id { get; set; } // Primary Key
		public string Sku { get; set; } // Stok Kodu
		public string Name { get; set; } // Ürün Adı
		public decimal Price { get; set; } // Fiyat
		public int Quantity { get; set; } // Miktar
		public int OrderId { get; set; } // Foreign Key
		public Order Order { get; set; } // Sipariş ilişkisi
	}
}
