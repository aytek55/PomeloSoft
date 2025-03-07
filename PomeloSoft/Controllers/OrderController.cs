using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PomeloSoft.Dtos;
using PomeloSoft.Models;
using PomeloSoft.PomeloDbContext;
using PomeloSoft.Services;

namespace PomeloSoft.Controllers
{	
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly AppDbContext _context;
		public OrderController(AppDbContext context)
		{
			_context = context;
		}
		[HttpPost("convert")]
		public IActionResult ConvertOrder([FromBody] OrderRequestDto orderRequestDto, Marketplace marketplaceType)
		{

			try
			{
				var adapter = new OrderAdapterService();
				var order = new Order();
				if (marketplaceType == Marketplace.Shopify)
				{
					// Shopify siparişi dönüştür
					order = adapter.ConvertFromShopify(orderRequestDto.ShopifyOrder);
				}
				if (marketplaceType == Marketplace.Trendyol)
				{
					// Trendyol siparişi dönüştür
					order = adapter.ConvertFromTrendyol(orderRequestDto.TrendyolOrder);
				}
				if (marketplaceType == Marketplace.Amazon)
				{
					// Amazon siparişi dönüştür
					order = adapter.ConvertFromAmazon(orderRequestDto.AmazonOrder);
				}

				return Ok(order);
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetOrders(
			[FromQuery] string? status,
			[FromQuery] string? marketplace,
			[FromQuery] string? date_range,
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10)
		{
			var query = _context.Orders.AsQueryable();

			// Status filtresi
			if (!string.IsNullOrEmpty(status) && Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
			{
				query = query.Where(o => o.Status == parsedStatus);
			}

			// Marketplace filtresi
			if (!string.IsNullOrEmpty(marketplace) && Enum.TryParse<Marketplace>(marketplace, true, out var parsedMarketplace))
			{
				query = query.Where(o => o.Marketplace == parsedMarketplace);
			}

			// Date Range filtresi
			if (!string.IsNullOrEmpty(date_range))
			{
				var dates = date_range.Split(',');
				if (dates.Length == 2 && DateTime.TryParse(dates[0], out var startDate) && DateTime.TryParse(dates[1], out var endDate))
				{
					query = query.Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate);
				}
			}

			// Sayfalama
			var totalRecords = await query.CountAsync();
			var orders = await query
				.OrderByDescending(o => o.CreatedAt)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.Select(o => new OrderDto
				{
					OrderId = o.Id,
					Customer = new Customer() {
						Name = o.Customer.Name,
						Id = o.Id,	
						Email = o.Customer.Email
					},
					TotalPrice = o.Items[0].Price, // Total Price için item içerisinde gez ve topla
					Status = o.Status,
					Marketplace = o.Marketplace
				})
				.ToListAsync();

			// Yanıt
			return Ok(new
			{
				totalRecords,
				page,
				pageSize,
				orders
			});
		}
	}
}
