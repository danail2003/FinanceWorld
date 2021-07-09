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

        public IActionResult All(int id = 1)
        {
            if (id < 1)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 8;

            var viewModel = new AllNewsViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetCount(),
                News = this.newsService.GetAll<NewsViewModel>(id, itemsPerPage),
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
