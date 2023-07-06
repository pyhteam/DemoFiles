using DemoFiles.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoFiles.Data
{
	public class HMZContext : DbContext
	{
		public HMZContext(DbContextOptions<HMZContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=.;Database=DemoFile;Trusted_Connection=True;MultipleActiveResultSets=true");
			}
		}
		//entities
		public DbSet<Product> Products { get; set; }
	}
}
