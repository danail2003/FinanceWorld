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

    public class NewsController : AdministrationController
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var createNewsDto = new CreateNewsInputModel
            {
                Categories = this.categoriesService.GetCategories(),
            };

            return this.View(createNewsDto);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateNewsDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.newsService.CreateAsync(dto, user.Id);

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.newsService.DeleteAsync(id);

            return this.Redirect("/News/All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var viewModel = this.newsService.GetById<EditNewsViewModel>(id);
            var categories = this.categoriesService.GetCategories();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditNewsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            await this.newsService.UpdateAsync(id, model);

            return this.Redirect("/News/All");
        }
    }
}
