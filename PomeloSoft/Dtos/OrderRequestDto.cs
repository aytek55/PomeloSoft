namespace PomeloSoft.Dtos
{
	public class OrderRequestDto
	{
		public ShopifyOrderDto ShopifyOrder { get; set; }
		public TrendyolOrderDto TrendyolOrder { get; set; }
		public AmazonOrderDto AmazonOrder { get; set; }
	}
}
