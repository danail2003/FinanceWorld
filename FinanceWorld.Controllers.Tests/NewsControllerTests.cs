using FinanceWorld.Data.Common.Repositories;
using FinanceWorld.Data.Models;
using FinanceWorld.Services.Data.Analyzes;
using FinanceWorld.Services.Data.News;
using FinanceWorld.Services.Mapping;
using FinanceWorld.Web.Areas.Administration.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinanceWorld.Controllers.Tests
{
    public class NewsControllerTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly List<News> news;
        private readonly NewsService newsService;

        public NewsControllerTests()
        {
            InitializeMapper();
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.newsService = new NewsService(this.mockNews.Object);
            this.news = new List<News>();
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));
        }


        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }
    }
}
