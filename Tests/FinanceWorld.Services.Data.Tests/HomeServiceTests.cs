namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Home;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using FinanceWorld.Web.ViewModels.Home;
    using FinanceWorld.Web.ViewModels.News;
    using Moq;
    using Xunit;

    public class HomeServiceTests
    {
        private readonly List<News> news;
        private readonly List<Analyze> analyzes;
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly HomeService homeService;

        public HomeServiceTests()
        {
            this.news = new List<News>();
            this.analyzes = new List<Analyze>();
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.homeService = new HomeService(this.mockNews.Object, this.mockAnalyzes.Object);
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
        }

        [Fact]
        public void WhenWeHaveOneAnalyzeInCollectionThereShouldBeReturnOneAnalyze()
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

            int count = this.analyzes.Count;

            Assert.Equal(1, count);
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

        [Fact]
        public void WhenWeHaveThreeNewsInCollectionThereShouldBeReturnThreeNews()
        {
            this.news.Add(new News
            {
                Id = 1,
                ImageUrl = "dsaasd",
                AddedByUserId = "1",
                Title = "dsaas",
                Content = "sadsa",
                CategoryId = 1,
                CreatedOn = DateTime.Now,
            });

            this.news.Add(new News
            {
                Id = 2,
                ImageUrl = "dsaasd",
                AddedByUserId = "2",
                Title = "dsaas",
                Content = "sadsa",
                CategoryId = 2,
                CreatedOn = DateTime.Now,
            });

            this.news.Add(new News
            {
                Id = 3,
                ImageUrl = "dsaasd",
                AddedByUserId = "3",
                Title = "dsaas",
                Content = "sadsa",
                CategoryId = 3,
                CreatedOn = DateTime.Now,
            });

            Assert.Equal(3, this.news.Count);
        }
    }
}
