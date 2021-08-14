namespace FinanceWorld.Controllers.Tests
{
    using Xunit;
    using FinanceWorld.Web.Controllers;
    using FinanceWorld.Web.ViewModels.Home;
    using MyTested.AspNetCore.Mvc;
    using FinanceWorld.Web.ViewModels;

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
