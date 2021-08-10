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
    using System.Threading.Tasks;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using FinanceWorld.Services.Mapping;
    using System.Reflection;

    public class HomeControllerTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly List<News> news;
        private readonly List<Analyze> analyzes;
        private readonly HomeService homeService;
        private readonly AnalyzesService analyzesService;
        private readonly Mock<IFormFile> fileMock;

        public HomeControllerTests()
        {
            InitializeMapper();
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.news = new List<News>();
            this.analyzes = new List<Analyze>();
            this.fileMock = new Mock<IFormFile>();
            this.analyzesService = new AnalyzesService(this.mockAnalyzes.Object);
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
            this.homeService = new HomeService(this.mockNews.Object, this.mockAnalyzes.Object);
        }

        [Fact]
        public async Task IndexMustReturnView()
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

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = InitializeFile("Hello", "test.png"), Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = InitializeFile("Hello", "test.png"), Description = "dfsdsdfas", Title = "test", }, "2", "test");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = InitializeFile("Hello", "test.png"), Description = "Test", Title = "test", }, "3", "test/test");

            var controller = new HomeController(this.homeService, GetMemoryCache("Home"));

            var view = controller.Index();

            Assert.NotNull(view);
        }
        
        private static IMemoryCache GetMemoryCache(object expectedValue)
        {
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                .Returns(true);
            return mockMemoryCache.Object;
        }

        private IFormFile InitializeFile(string imageContent, string fileName)
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);

            return this.fileMock.Object;
        }

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }
    }
}
