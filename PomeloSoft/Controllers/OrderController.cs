using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PomeloSoft.Dtos;
using PomeloSoft.Models;
using PomeloSoft.Services;

namespace PomeloSoft.Controllers
{	
	[ApiController]
	[Route("api/[controller]")]
	public class OrderConversionController : ControllerBase
	{
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
	}
}
