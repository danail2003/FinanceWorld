namespace FinanceWorld.Web.Controllers
{
    using System;

    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Web.ViewModels.Categories;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
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
                throw new InvalidOperationException("The pages starts from one!");
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
            NewsByIdViewModel viewModel;

            try
            {
                viewModel = this.newsService.GetById<NewsByIdViewModel>(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return this.View(viewModel);
        }

        public IActionResult NewsByCategory([FromQuery] SearchByCategoriesViewModel model, int id = 1)
        {
            const int itemsPerPage = 8;

            var viewModel = new SearchByCategoriesViewModel
            {
                Name = model.Name,
                News = this.newsService.GetByCategory<NewsViewModel>(model.Name, id, itemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
