namespace FinanceWorld.Controllers.Tests
{
    using System;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Services.Messaging;
    using FinanceWorld.Web.Controllers;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AnalyzesControllerTests
    {
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly AnalyzesService analyzesService;

        public AnalyzesControllerTests()
        {
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.analyzesService = new AnalyzesService(this.mockAnalyzes.Object);
        }

        [Fact]
        public void GetCreateShouldBeForAuthorizedUsersAndShouldReturnView()
        {
            MyController<AnalyzesController>
                .Instance(i => i
                .WithDependencies(d => d
                .WithNo<IEmailSender>()
                .WithNo<IAnalyzesService>()
                .WithNo<IWebHostEnvironment>()
                .WithNo<UserManager<ApplicationUser>>()))
                .Calling(x => x.Create())
                .ShouldHave()
                .ActionAttributes(attr => attr
                .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PostCreateShouldThrowExceptionWhenInvalidDataIsGiven()
        {
            MyController<AnalyzesController>
                .Instance(i => i
                .WithDependencies(d => d
                .WithNo<IEmailSender>()
                .WithNo<IAnalyzesService>()
                .WithNo<IWebHostEnvironment>()
                .WithNo<UserManager<ApplicationUser>>()))
                .Calling(x => x.Create(new CreateAnalyzeInputModel
                {
                    Description = "test",
                    Title = "test"
                }))
                .ShouldThrow()
                .Exception();
        }

        [Fact]
        public void AllShouldReturnExceptionWhenPageIsBelowOne()
        {
            MyController<AnalyzesController>
                .Instance(i => i
                .WithDependencies(d => d
                .WithNo<IEmailSender>()
                .WithNo<IAnalyzesService>()
                .WithNo<IWebHostEnvironment>()
                .WithNo<UserManager<ApplicationUser>>()))
                .Calling(x => x.All(0))
                .ShouldThrow()
                .Exception()
                .OfType<InvalidOperationException>();
        }

        [Fact]
        public void ByIdShouldReturnView()
        {
            MyController<AnalyzesController>
                .Instance(i => i
                .WithDependencies(d => d
                .With<IAnalyzesService>(this.analyzesService)
                .WithNo<IEmailSender>()
                .WithNo<IWebHostEnvironment>()
                .WithNo<UserManager<ApplicationUser>>())
                .WithData(new Analyze
                {
                    AddedByUserId = "123",
                    Description = "test",
                    CreatedOn = DateTime.UtcNow,
                    Id = "1",
                    Image = new Image(),
                    Title = "Test",
                    ModifiedOn = DateTime.Now,
                }))
                .Calling(x => x.ById("1"))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void DeleteShouldReturnRedirect()
        {
            MyController<AnalyzesController>
                .Instance(i => i
                .WithDependencies(d => d
                .With<IAnalyzesService>(this.analyzesService)
                .WithNo<IEmailSender>()
                .WithNo<IWebHostEnvironment>()
                .WithNo<UserManager<ApplicationUser>>())
                .WithData(new Analyze
                {
                    AddedByUserId = TestUser.Identifier,
                    Description = "test",
                    CreatedOn = DateTime.UtcNow,
                    Id = "1",
                    Image = new Image(),
                    Title = "Test",
                    ModifiedOn = DateTime.Now,
                }))
                .Calling(x => x.Delete("1"))
                .ShouldReturn()
                .Redirect();
        }

        [Fact]
        public void SearchAnalyzeShouldReturnView()
        {
            MyController<AnalyzesController>
                .Instance(i => i
                .WithDependencies(d => d
                .With<IAnalyzesService>(this.analyzesService)
                .WithNo<IEmailSender>()
                .WithNo<IWebHostEnvironment>()
                .WithNo<UserManager<ApplicationUser>>())
                .WithData(new Analyze
                {
                    AddedByUserId = TestUser.Identifier,
                    Description = "test",
                    CreatedOn = DateTime.UtcNow,
                    Id = "1",
                    Image = new Image(),
                    Title = "Test",
                    ModifiedOn = DateTime.Now,
                }))
                .Calling(x => x.SearchAnalyze(new AllAnalyzesViewModel
                {
                    SearchTitle="Test",
                }, 1))
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<AllAnalyzesViewModel>());
        }
    }
}
