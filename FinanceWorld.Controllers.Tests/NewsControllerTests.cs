using FinanceWorld.Common;
using FinanceWorld.Data.Common.Repositories;
using FinanceWorld.Data.Models;
using FinanceWorld.Services.Data.Analyzes;
using FinanceWorld.Services.Data.Models;
using FinanceWorld.Services.Data.News;
using FinanceWorld.Services.Mapping;
using FinanceWorld.Web.Areas.Administration.Controllers;
using FinanceWorld.Web.Controllers;
using FinanceWorld.Web.ViewModels.News;
using Moq;
using MyTested.AspNetCore.Mvc;
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
    }
}
