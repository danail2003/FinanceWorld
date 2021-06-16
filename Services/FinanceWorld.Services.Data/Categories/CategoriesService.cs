namespace FinanceWorld.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetCategories()
        {
            return this.categoriesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
