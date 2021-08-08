namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.News;
    using Moq;
    using Xunit;

    public class NewsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly List<News> news;
        private readonly NewsService newsService;

        public NewsServiceTests()
        {
            InitializeMapper();
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.news = new List<News>();
            this.newsService = new NewsService(this.mockNews.Object);
            this.mockNews.Setup(x => x.All()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockNews.Setup(x => x.Delete(It.IsAny<News>())).Callback((News news) => this.news.Remove(news));
        }

        [Fact]
        public async Task CreateMethodShouldCreateExactNewsWithExactValues()
        {
            var news = await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");

            Assert.Single(this.news);
            Assert.Equal("test", news.Title);
            Assert.Equal("testtt", news.Content);
            Assert.Equal(1, news.CategoryId);
            Assert.Equal("test", news.ImageUrl);
            Assert.Equal("1", news.AddedByUserId);
        }

        [Fact]
        public async Task InTheCollectionShouldBeExactlyThreeEntries()
        {
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");

            Assert.Equal(3, this.news.Count);
        }

        [Fact]
        public async Task GetCountMethodShouldReturnCorrectValue()
        {
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");

            Assert.Equal(3, this.newsService.GetCount());
            Assert.False(this.newsService.GetCount() != 3);
        }

        [Fact]
        public async Task DeleteMethodShouldWorkProperly()
        {
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");
            await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");
            var deletedNews = await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");

            await this.newsService.DeleteAsync(deletedNews.Id);

            Assert.Equal(2, this.news.Count);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperly()
        {
            var news = await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");

            await this.newsService.UpdateAsync(news.Id, new CreateEditNewsInputModel { Content = "testt", Title = "test2", CategoryId = 2, ImageUrl = "test2" });

            Assert.Equal("testt", news.Content);
            Assert.Equal("test2", news.Title);
            Assert.Equal(2, news.CategoryId);
            Assert.Equal("test2", news.Title);
        }

        [Fact]
        public async Task UpdateMethodShouldNotChangeValueIfItIsIsGivenSameValue()
        {
            var news = await this.newsService.CreateAsync(new CreateNewsDto { Content = "testtt", Title = "test", CategoryId = 1, ImageUrl = "test" }, "1");

            await this.newsService.UpdateAsync(news.Id, new CreateEditNewsInputModel { Content = "testtt", Title = "test2", CategoryId = 2, ImageUrl = "test2" });

            Assert.Equal("testtt", news.Content);
        }

        [Fact]
        public void GetAllMethodShouldReturnCorrectCount()
        {
            this.news.Add(new News
            {
                Category = new Category(),
                AddedByUser = new ApplicationUser(),
                AddedByUserId = "1",
                CategoryId = 1,
                Content = "test",
                CreatedOn = DateTime.UtcNow,
                Id = 123,
                ImageUrl = "test.com",
                Title = "test",
                IsDeleted = false,
            });

            this.news.Add(new News
            {
                Category = new Category(),
                AddedByUser = new ApplicationUser(),
                AddedByUserId = "2",
                CategoryId = 2,
                Content = "test",
                CreatedOn = DateTime.UtcNow,
                Id = 124,
                ImageUrl = "test2.com",
                Title = "test2",
                IsDeleted = false,
            });

            var result = this.newsService.GetAll<NewsViewModel>(1, 8);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllShouldReturnCorrectValues()
        {
            this.news.Add(new News
            {
                Category = new Category(),
                AddedByUser = new ApplicationUser(),
                AddedByUserId = "1",
                CategoryId = 1,
                Content = "test",
                CreatedOn = DateTime.UtcNow,
                Id = 123,
                ImageUrl = "test.com",
                Title = "test",
                IsDeleted = false,
            });

            this.news.Add(new News
            {
                Category = new Category { Name = "Trends" },
                AddedByUser = new ApplicationUser(),
                AddedByUserId = "2",
                CategoryId = 2,
                Content = "test",
                CreatedOn = DateTime.UtcNow,
                Id = 124,
                ImageUrl = "test2.com",
                Title = "test2",
                IsDeleted = false,
            });

            var result = this.newsService.GetAll<NewsViewModel>(1, 8).ToList();

            Assert.Equal("test2", result[0].Title);
            Assert.Equal("test.com", result[1].ImageUrl);
            Assert.Equal("test", result[0].Content);
            Assert.Equal(123, result[1].Id);
            Assert.Equal("Trends", result[0].CategoryName);
            Assert.Equal(DateTime.UtcNow.ToString("d"), result[1].CreatedOnFormatted);
        }

        [Fact]
        public void GetByIdShouldReturnCorrectNews()
        {
            this.news.Add(new News
            {
                Category = new Category(),
                AddedByUser = new ApplicationUser(),
                AddedByUserId = "2",
                CategoryId = 2,
                Content = "test",
                CreatedOn = DateTime.UtcNow,
                Id = 124,
                ImageUrl = "test2.com",
                Title = "test2",
                IsDeleted = false,
            });

            var result = this.newsService.GetById<NewsViewModel>(124);

            Assert.Equal("test2", result.Title);
            Assert.Equal(124, result.Id);
        }

        [Fact]
        public void GetCountByCategoryShouldReturnCorrectCount()
        {
            this.news.Add(new News
            {
                CategoryId = 1,
                Content = "test",
                Category = new Category { Name = "Trends" },
                AddedByUserId = "1",
                Title = "Test",
                ImageUrl = "testest.com",
            });

            this.news.Add(new News
            {
                CategoryId = 2,
                Content = "test2",
                Category = new Category { Name = "Trends" },
                AddedByUserId = "1",
                Title = "Test2",
                ImageUrl = "testest2.com",
            });

            var result = this.newsService.GetCountByCategory("Trends");

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetByCategoryShouldReturnCorrectCount()
        {
            this.news.Add(new News
            {
                CategoryId = 1,
                Content = "test",
                Category = new Category { Name = "Trends" },
                AddedByUserId = "1",
                Title = "Test",
                ImageUrl = "testest.com",
            });

            this.news.Add(new News
            {
                CategoryId = 2,
                Content = "test2",
                Category = new Category { Name = "Trends" },
                AddedByUserId = "1",
                Title = "Test2",
                ImageUrl = "testest2.com",
            });

            var result = this.newsService.GetByCategory<NewsViewModel>("trends", 1, 8);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByCategoryShouldReturnCorrectValues()
        {
            this.news.Add(new News
            {
                CategoryId = 1,
                Content = "test",
                Category = new Category { Name = "Trends" },
                AddedByUserId = "1",
                Title = "Test",
                ImageUrl = "testest.com",
            });

            this.news.Add(new News
            {
                CategoryId = 2,
                Content = "test2",
                Category = new Category { Name = "Trends" },
                AddedByUserId = "1",
                Title = "Test2",
                ImageUrl = "testest2.com",
            });

            var result = this.newsService.GetByCategory<NewsViewModel>("trends", 1, 8).ToList();

            Assert.Equal("Test", result[0].Title);
            Assert.Equal("testest2.com", result[1].ImageUrl);
        }

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }
    }
}
