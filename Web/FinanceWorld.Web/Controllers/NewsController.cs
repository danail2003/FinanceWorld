namespace FinanceWorld.Web.Controllers
{
    using System;

    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Web.ViewModels.Categories;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private const int ItemsPerPage = 8;
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult All(int id = 1)
        {
            if (id < 1)
            {
                return this.BadRequest();
            }

            var viewModel = new AllNewsViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetCount(),
                News = this.newsService.GetAll<NewsViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            NewsViewModel viewModel;

            try
            {
                viewModel = this.newsService.GetById<NewsViewModel>(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return this.View(viewModel);
        }

        public IActionResult NewsByCategory([FromQuery] SearchByCategoriesViewModel model, int id = 1)
        {
            var viewModel = new SearchByCategoriesViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.newsService.GetCountByCategory(model.Name),
                Name = model.Name,
                News = this.newsService.GetByCategory<NewsViewModel>(model.Name, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
