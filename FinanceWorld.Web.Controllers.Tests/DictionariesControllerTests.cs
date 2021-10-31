namespace FinanceWorld.Web.Controllers.Tests
{
    using System;

    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using FinanceWorld.Web.ViewModels.Dictionaries;
    using FinanceWorld.Common;
    using FinanceWorld.Services.Data.Models;

    public class DictionariesControllerTests
    {
        [Fact]
        public void ListShouldReturnView()
        {
            MyController<Web.Controllers.DictionariesController>
                .Instance()
                .Calling(x => x.List())
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<DictionaryListViewModel>());
        }

        [Fact]
        public void ByIdShouldReturnViewWithCorrectValue()
        {
            MyController<Web.Controllers.DictionariesController>
                .Instance(instance => instance
                .WithData(new Data.Models.Dictionary
                {
                    AddedByUserId = "1",
                    Description = "test",
                    Name = "test",
                    Id = "123",
                    CreatedOn = DateTime.UtcNow,
                }))
                .Calling(x => x.ById("123"))
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<TermByIdViewModel>()
                .Passing(term => term.Name == "test"));
        }

        [Fact]
        public void GetCreateShouldBeForAdminsAndShouldReturnView()
        {
            MyController<Web.Areas.Administration.Controllers.DictionariesController>
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
        public void PostCreateShouldThrowExceptionWithInvalidData()
        {
            MyController<Web.Areas.Administration.Controllers.DictionariesController>
                .Instance()
                .Calling(x => x.Create(With.Default<CreateDictionaryDto>()))
                .ShouldThrow()
                .Exception();
        }
    }
}
