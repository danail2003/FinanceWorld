namespace FinanceWorld.Web.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Categories;
    using FinanceWorld.Web.ViewModels.News;

    using Moq;
    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class NewsControllerTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNews;
        private readonly List<News> news;

        public NewsControllerTests()
        {
            this.mockNews = new Mock<IDeletableEntityRepository<News>>();
            this.news = new List<News>();            
        }

        [Fact]
        public void GetCreateShouldBeForAdminsAndShouldReturnView()
        {
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance()
                .Calling(x => x.Create())
                .ShouldHave()
                .ActionAttributes(attr => attr
                .RestrictingForAuthorizedRequests(GlobalConstants.AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PostCreateShouldBeForAdminsAndReturnRedirectWithValidData()
        {
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance(c => c
                .WithUser())
                .Calling(x => x.Create(new CreateNewsDto
                {
                    Title = "test",
                    Content = "testtest",
                    CategoryId = 1,
                    ImageUrl = "test.com"
                }));
        }

        [Theory]
        [InlineData(12)]
        public void DeleteShouldBeForAdminsAndShouldReturnView(int id)
        {
            this.mockNews.Setup(x => x.AllAsNoTracking()).Returns(this.news.AsQueryable());
            this.mockNews.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => this.news.Add(news));

            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance(x => x.WithData(new News
                {
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    Id = 12,
                    ImageUrl = "test.com",
                    AddedByUserId = "1",
                    CreatedOn = DateTime.UtcNow
                }))
                .Calling(x => x.Delete(id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                .RestrictingForAuthorizedRequests(GlobalConstants.AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .Redirect();
        }

        [Theory]
        [InlineData(1)]
        public void GetEditShouldBeForAdminsAndShouldReturnView(int id)
        {
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance(x => x.WithData(new News
                {
                    CategoryId = 1,
                    Content = "test",
                    ImageUrl = "test.com",
                    Title = "test",
                    AddedByUserId = "1",
                    Id = id,
                    CreatedOn = DateTime.UtcNow,
                }))
                .Calling(x => x.Edit(id))
                .ShouldHave()
                .ActionAttributes(attr => attr
                .RestrictingForAuthorizedRequests(GlobalConstants.AdministratorRoleName))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Theory]
        [InlineData(1)]
        public void PostEditShoulBeForAdminsAndReturnRedirect(int id)
        {
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance(x => x.WithData(new News
                {
                    AddedByUserId = "1",
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    Id = id,
                    ImageUrl = "test.com",
                    CreatedOn = DateTime.UtcNow,
                }))
                .Calling(x => x.Edit(id, new CreateEditNewsInputModel
                {
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    ImageUrl = "test.com",
                }))
                .ShouldHave()
                .ActionAttributes(attr => attr
                .RestrictingForAuthorizedRequests(GlobalConstants.AdministratorRoleName)
                .RestrictingForHttpMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Redirect();
        }

        [Theory]
        [InlineData(1)]
        public void PostEditShoulThrowException(int id)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance(x => x.WithData(new News
                {
                    AddedByUserId = "1",
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    Id = id,
                    ImageUrl = "test.com",
                    CreatedOn = DateTime.UtcNow,
                }))
                .Calling(x => x.Edit(id, new CreateEditNewsInputModel
                {
                    CategoryId = 1,
                    Content = null,
                    Title = "test",
                    ImageUrl = "test.com",
                }))
                .ShouldThrow()
                .Exception();
        }

        [Fact]
        public void AllShouldReturnView()
        {
            MyController<Web.Controllers.NewsController>
                .Instance(x => x
                .WithData(new News
                {
                    AddedByUserId = "1",
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    ImageUrl = "test.com",
                    CreatedOn = DateTime.UtcNow,
                    Id = 1,
                }))
                .Calling(x => x.All(1))
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<AllNewsViewModel>());
        }

        [Fact]
        public void AllShouldThrowException()
        {
            MyController<Web.Controllers.NewsController>
                .Instance(x => x
                .WithData(new News
                {
                    AddedByUserId = "1",
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    ImageUrl = "test.com",
                    CreatedOn = DateTime.UtcNow,
                    Id = 1,
                }))
                .Calling(x => x.All(-1))
                .ShouldThrow()
                .Exception()
                .OfType<InvalidOperationException>();
        }

        [Fact]
        public void ByIdShouldReturnView()
        {
            MyController<Web.Controllers.NewsController>
                .Instance(x => x
                .WithData(new News
                {
                    AddedByUserId = "1",
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    ImageUrl = "test.com",
                    CreatedOn = DateTime.UtcNow,
                    Id = 1,
                }))
                .Calling(x => x.ById(1))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void NewsByCategoryShouldReturnView()
        {
            MyController<Web.Controllers.NewsController>
                .Instance(x => x
                .WithData(new News
                {
                    AddedByUserId = "1",
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    ImageUrl = "test.com",
                    CreatedOn = DateTime.UtcNow,
                    Id = 1,
                }))
                .Calling(x => x.NewsByCategory(new SearchByCategoriesViewModel
                {
                    Name = "test"
                }, 1))
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<SearchByCategoriesViewModel>());
        }
    }
}
