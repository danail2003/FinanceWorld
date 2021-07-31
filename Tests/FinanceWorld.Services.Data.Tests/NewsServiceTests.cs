namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Data.News;
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
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.news = new List<News>();
            this.newsService = new NewsService(this.mockNews.Object);
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.All()).Returns(this.news.AsQueryable());
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
    }
}
