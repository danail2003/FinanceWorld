namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Categories;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class NewsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly INewsService newsService;

        public NewsController(
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            INewsService newsService)
        {
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.newsService = newsService;
        }

        public IActionResult Create()
        {
            var createNewsDto = new CreateEditNewsInputModel
            {
                Categories = this.categoriesService.GetCategories(),
            };

            return this.View(createNewsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.newsService.CreateAsync(dto, user.Id);

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully added news!";

            return this.Redirect("/");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.newsService.DeleteAsync(id);

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully delete!";

            return this.Redirect("/News/All");
        }

        public IActionResult Edit(int id)
        {
            var viewModel = this.newsService.GetById<CreateEditNewsInputModel>(id);
            var categories = this.categoriesService.GetCategories();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateEditNewsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            await this.newsService.UpdateAsync(id, model);

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully edited news!";

            return this.Redirect("/News/All");
        }
    }
}
