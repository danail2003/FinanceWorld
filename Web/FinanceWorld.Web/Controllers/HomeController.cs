namespace FinanceWorld.Web.Controllers
{
    using System.Diagnostics;

    using FinanceWorld.Services.Data.Home;
    using FinanceWorld.Web.ViewModels;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using FinanceWorld.Web.ViewModels.Home;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Analyzes = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>(),
                News = this.homeService.GetLastThreeNews<NewsViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
