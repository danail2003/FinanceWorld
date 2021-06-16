namespace FinanceWorld.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Company News" });
            await dbContext.Categories.AddAsync(new Category { Name = "Markets News" });
            await dbContext.Categories.AddAsync(new Category { Name = "Trading News" });
            await dbContext.Categories.AddAsync(new Category { Name = "Political News" });
            await dbContext.Categories.AddAsync(new Category { Name = "Trends" });

            await dbContext.SaveChangesAsync();
        }
    }
}
