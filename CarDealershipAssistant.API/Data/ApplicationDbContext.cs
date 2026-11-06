using CarDealershipAssistant.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAssistant.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
      DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

        public DbSet<Car> Cars => Set<Car>();
    }
}