using Microsoft.EntityFrameworkCore;

namespace parallax_tickets_api.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
		{ 

		}
	}
}
