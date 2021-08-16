namespace FinanceWorld.Controllers.Tests
{
    using FinanceWorld.Web.Controllers;
    using FinanceWorld.Web.ViewModels.Home;
    using FinanceWorld.Web.ViewModels;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            MyController<HomeController>
                .Instance()
                .Calling(x => x.Index())
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<HomeViewModel>());
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            MyController<HomeController>
                .Instance()
                .Calling(x => x.Error())
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<ErrorViewModel>());
        }
    }
}
