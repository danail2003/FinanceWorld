namespace FinanceWorld.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Categories;
    using Moq;
    using Xunit;

    public class CategoriesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Category>> mockCategories;
        private readonly List<Category> categories;
        private readonly CategoriesService categoriesService;

        public CategoriesServiceTest()
        {
            this.mockCategories = new Mock<IDeletableEntityRepository<Category>>();
            this.categories = new List<Category>();
            this.categoriesService = new CategoriesService(this.mockCategories.Object);
            this.mockCategories.Setup(x => x.AllAsNoTracking()).Returns(this.categories.AsQueryable());
            this.mockCategories.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback((Category category) => this.categories.Add(category));
        }

        [Fact]
        public void GetCategoriesShouldWorkCorrectCount()
        {
            this.categories.Add(new Category { Id = 1, Name = "Trends" });
            this.categories.Add(new Category { Id = 2, Name = "Political" });
            this.categories.Add(new Category { Id = 3, Name = "Trading" });

            var result = this.categoriesService.GetCategories();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetCategoriesShouldWorkCorrectValues()
        {
            this.categories.Add(new Category { Id = 1, Name = "Trends" });
            this.categories.Add(new Category { Id = 2, Name = "Political" });
            this.categories.Add(new Category { Id = 3, Name = "Trading" });

            var result = this.categoriesService.GetCategories().ToList();

            Assert.Equal("Trends", result[0].Value);
            Assert.Equal("1", result[0].Key);
            Assert.Equal("Political", result[1].Value);
            Assert.Equal("2", result[1].Key);
            Assert.Equal("Trading", result[2].Value);
            Assert.Equal("3", result[2].Key);
        }
    }
}
