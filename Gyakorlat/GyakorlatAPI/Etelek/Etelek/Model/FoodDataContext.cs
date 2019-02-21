using Microsoft.EntityFrameworkCore;

namespace Etelek.Model
{
    public class FoodDataContext : DbContext
    {
        public FoodDataContext(DbContextOptions<FoodDataContext> options) : base(options)
        { }

        public DbSet<Food> Food { get; set; }
    }
}
