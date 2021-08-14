namespace FinanceWorld.Controllers.Tests
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Services.Messaging;
    using FinanceWorld.Web.Controllers;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AnalyzesControllerTests
    {
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
    }
}
