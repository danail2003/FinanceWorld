namespace FinanceWorld.Web.Controllers
{
    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult All()
        {
            var viewModel = new AllNewsViewModel
            {
                News = this.newsService.GetAll<NewsViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.newsService.GetById<NewsByIdViewModel>(id);

            return this.View(viewModel);
        }
    }
}
