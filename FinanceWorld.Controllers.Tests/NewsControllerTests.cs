namespace FinanceWorld.Controllers.Tests
{
    using System;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.News;
    using FinanceWorld.Web.ViewModels.Categories;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class NewsControllerTests
    {
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
        public void PostCreateShouldThrowException()
        {
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance()
                .Calling(x => x.Create(With.Default<CreateNewsDto>()))
                .ShouldThrow()
                .Exception();
        }

        [Theory]
        [InlineData(12)]
        public void DeleteShouldBeForAdminsAndShouldReturnView(int id)
        {
            MyController<Web.Areas.Administration.Controllers.NewsController>
                .Instance(x => x.WithData(new News
                {
                    CategoryId = 1,
                    Content = "test",
                    Title = "test",
                    Id = id,
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
