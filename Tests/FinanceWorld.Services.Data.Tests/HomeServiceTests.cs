namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Home;
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
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
        }

        [Fact]
        public void GetThreeLastAnalyzesShouldReturnCorrectValues()
        {
            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "123",
                CreatedOn = DateTime.UtcNow,
                Id = "1",
                Image = new Image(),
                Description = "testtest2",
                Title = "test2",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "testtest",
                Title = "test",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            var result = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>().ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("test", result[0].Title);
            Assert.Equal("test2", result[1].Title);
            Assert.Equal("testtest", result[0].Description);
            Assert.Equal("testtest2", result[1].Description);
        }

        [Fact]
        public void GetThreeLastNewsShouldReturnCorrectCount()
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

            var result = this.homeService.GetLastThreeNews<NewsViewModel>();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetThreeLastNewsShouldReturnCorrectValues()
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

            var result = this.homeService.GetLastThreeNews<NewsViewModel>().ToArray();

            Assert.Equal("Test", result[0].Title);
            Assert.Equal("test2", result[1].Content);
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
