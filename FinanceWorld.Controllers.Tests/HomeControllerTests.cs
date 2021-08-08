namespace FinanceWorld.Controllers.Tests
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Data.Common.Repositories;
    using System;
    using System.Linq;
    using Xunit;
    using Moq;
    using System.Collections.Generic;
    using FinanceWorld.Services.Data.Home;
    using FinanceWorld.Web.Controllers;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeControllerTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly List<News> news;
        private readonly List<Analyze> analyzes;
        private readonly HomeService homeService;

        public HomeControllerTests()
        {
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.news = new List<News>();
            this.analyzes = new List<Analyze>();
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
            this.homeService = new HomeService(this.mockNews.Object, this.mockAnalyzes.Object);
        }

        [Fact]
        public void ControllerShouldHaveReturnResult()
        {
            this.analyzes.Add(new Analyze
            {
                Id = "1",
                ImageId = "1",
                AddedByUserId = "1",
                Description = "das",
                CreatedOn = DateTime.Now,
                Title = "dsa",
            });

            this.analyzes.Add(new Analyze
            {
                Id = "3",
                ImageId = "6",
                AddedByUserId = "2",
                Description = "das",
                CreatedOn = DateTime.Now,
                Title = "dsa",
            });

            this.analyzes.Add(new Analyze
            {
                Id = "2",
                ImageId = "5",
                AddedByUserId = "5",
                Description = "das",
                CreatedOn = DateTime.Now,
                Title = "dsa",
            });

            var contoller = new HomeController(this.homeService, GetMemoryCache(true));
            

            
        }

        public static IMemoryCache GetMemoryCache(object expectedValue)
        {
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                .Returns(true);
            return mockMemoryCache.Object;
        }
    }
}
