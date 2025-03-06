using PomeloSoft.Dtos;
using PomeloSoft.Models;

namespace PomeloSoft.Services
{
	public class OrderAdapterService
	{
		public Order ConvertFromShopify(ShopifyOrderDto shopifyOrder)
		{
			return new Order
			{
				OrderNumber = shopifyOrder.Id,
				Customer = new Customer
				{
					Name = $"{shopifyOrder.Customer.FirstName} {shopifyOrder.Customer.LastName}",
					Email = shopifyOrder.Customer.Email
				},
				Items = shopifyOrder.LineItems.Select(p => new OrderItem
				{
					Sku = p.ProductId,
					Name = p.Name,
					Price = p.Price,
					Quantity = p.Quantity
				}).ToList(),
				Status = ConvertShopifyStatus(shopifyOrder.FinancialStatus),
				Marketplace = Marketplace.Shopify,
				CreatedAt = shopifyOrder.CreatedAt
			};
		}

		private OrderStatus ConvertShopifyStatus(string financialStatus)
		{
			return financialStatus switch
			{
				"paid" => OrderStatus.Shipped,
				"pending" => OrderStatus.Pending,
				"refunded" => OrderStatus.Canceled,
				_ => OrderStatus.Pending
			};
		}

		public Order ConvertFromTrendyol(TrendyolOrderDto trendyolOrder)
		{
			return new Order
			{
				OrderNumber = trendyolOrder.OrderNumber.ToString(),
				Customer = new Customer
				{
					Name = trendyolOrder.Buyer.FullName,
					Email = trendyolOrder.Buyer.ContactEmail
				},
				Items = trendyolOrder.Products.Select(p => new OrderItem
				{
					Sku = p.Sku,
					Name = p.Title,
					Price = (decimal)p.SalePrice,
					Quantity = p.Amount
				}).ToList(),
				Status = ConvertTrendyolStatus(trendyolOrder.Status),
				Marketplace = Marketplace.Trendyol,
				CreatedAt = DateTime.Parse(trendyolOrder.OrderDate)
			};
		}

		private OrderStatus ConvertTrendyolStatus(string status)
		{
			return status switch
			{
				"Awaiting" => OrderStatus.Pending,
				"Shipped" => OrderStatus.Shipped,
				"Completed" => OrderStatus.Delivered,
				_ => OrderStatus.Pending
			};
		}

		public Order ConvertFromAmazon(AmazonOrderDto amazonOrder)
		{
			return new Order
			{
				OrderNumber = amazonOrder.OrderId,
				Customer = new Customer
				{
					Name = amazonOrder.Buyer.Name,
					Email = amazonOrder.Buyer.Email
				},
				Items = amazonOrder.Items.Select(p => new OrderItem
				{
					Sku = p.Asin,
					Name = p.Description,
					Price = p.Cost,
					Quantity = p.Count
				}).ToList(),
				Status = ConvertAmazonStatus(amazonOrder.OrderStatus),
				Marketplace = Marketplace.Amazon,
				CreatedAt = amazonOrder.PurchaseDate
			};
		}

		private OrderStatus ConvertAmazonStatus(string status)
		{
			return status switch
			{
				"Pending" => OrderStatus.Pending,
				"Shipped" => OrderStatus.Shipped,
				"Delivered" => OrderStatus.Delivered,
				"Cancelled" => OrderStatus.Canceled,
				_ => OrderStatus.Pending
			};
		}
	}

}
