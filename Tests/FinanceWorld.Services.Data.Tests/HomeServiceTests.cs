namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Services.Data.Home;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class HomeServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly List<News> news;
        private readonly List<Analyze> analyzes;
        private readonly HomeService homeService;
        private readonly AnalyzesService anazesService;
        private readonly NewsService newsService;
        private readonly Mock<IFormFile> fileMock;

        public HomeServiceTests()
        {
            InitializeMapper();
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.fileMock = new Mock<IFormFile>();
            this.news = new List<News>();
            this.analyzes = new List<Analyze>();
            this.homeService = new HomeService(this.mockNews.Object, this.mockAnalyzes.Object);
            this.anazesService = new AnalyzesService(this.mockAnalyzes.Object);
            this.newsService = new NewsService(this.mockNews.Object);
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
        }

        [Fact]
        public async Task MethodGetThreeLastAnalyzesShouldBeNotNull()
        {
            await this.anazesService.CreateAsync(new CreateAnalyzeInputModel { Title = "test", Description = "testtest", Image = this.InitializeFile("Hello", "test.gif") }, "1", "test");

            var analyzes = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>();

            Assert.NotNull(analyzes);
        }

        [Fact]
        public async Task GetThreeLastAnalyzesShouldReturnCorrectCount()
        {
            await this.anazesService.CreateAsync(new CreateAnalyzeInputModel { Title = "test", Description = "testtest", Image = this.InitializeFile("Hello", "test.gif") }, "1", "test");
            await this.anazesService.CreateAsync(new CreateAnalyzeInputModel { Title = "test", Description = "testtest", Image = this.InitializeFile("Hello", "test.gif") }, "1", "test");

            var result = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetThreeLastAnalyzesShouldReturnCorrectValues()
        {
            await this.anazesService.CreateAsync(new CreateAnalyzeInputModel { Title = "test", Description = "testtest", Image = this.InitializeFile("Hello", "test.gif") }, "1", "test");
            await this.anazesService.CreateAsync(new CreateAnalyzeInputModel { Title = "test2", Description = "testtest2", Image = this.InitializeFile("Hello", "test.gif") }, "1", "test");

            var result = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>().ToList();

            Assert.Equal("test", result[0].Title);
            Assert.Equal("test2", result[1].Title);
            Assert.Equal("testtest", result[0].Description);
            Assert.Equal("testtest2", result[1].Description);
        }

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
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
    }
}
