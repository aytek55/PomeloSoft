using Microsoft.EntityFrameworkCore;
using PomeloSoft.Models;

namespace PomeloSoft.PomeloDbContext
{
	public class AppDbContext : DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Customer> Customers { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>()
				.HasOne(o => o.Customer)
				.WithMany()
				.OnDelete(DeleteBehavior.Cascade); // Müşteri silinirse siparişler de silinir

			modelBuilder.Entity<Order>()
				.HasMany(o => o.Items)
				.WithOne(i => i.Order)
				.HasForeignKey(i => i.OrderId);
		}
	}

}
