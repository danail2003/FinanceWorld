namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using FinanceWorld.Services.Data.Home;
    using FinanceWorld.Web.ViewModels;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using FinanceWorld.Web.ViewModels.Home;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IMemoryCache memoryCache;

        public HomeController(
            IHomeService homeService,
            IMemoryCache memoryCache)
        {
            this.homeService = homeService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            if (!this.memoryCache.TryGetValue<HomeViewModel>("Home", out var viewModel))
            {
                viewModel = new HomeViewModel
                {
                    Analyzes = this.homeService.GetLastThreeAnalyzes<AnalyzesViewModel>(),
                    News = this.homeService.GetLastThreeNews<NewsViewModel>(),
                };

                this.memoryCache.Set("Home", viewModel, new MemoryCacheEntryOptions { SlidingExpiration = new TimeSpan(0, 1, 0) });
            }

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
