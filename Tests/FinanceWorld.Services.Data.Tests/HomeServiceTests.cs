namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Home;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Moq;
    using Xunit;

    public class HomeServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly List<News> news;
        private readonly List<Analyze> analyzes;
        private readonly HomeService homeService;

        public HomeServiceTests()
        {
            InitializeMapper();
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.news = new List<News>();
            this.analyzes = new List<Analyze>();
            this.homeService = new HomeService(this.mockNews.Object, this.mockAnalyzes.Object);
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
        }

        [Fact]
        public void MethodGetThreeLastAnalyzesShouldBeNotNull()
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

            var analyzes = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>();

            Assert.NotNull(analyzes);
        }

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }
    }
}
