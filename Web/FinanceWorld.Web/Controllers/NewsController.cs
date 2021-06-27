﻿namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Categories;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Data.News;
    using FinanceWorld.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;

        public NewsController(
            INewsService newsService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService)
        {
            this.newsService = newsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var createNewsDto = new CreateNewsInputModel
            {
                Categories = this.categoriesService.GetCategories(),
            };

            return this.View(createNewsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsDto dto)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.newsService.CreateAsync(dto, user.Id);

            return this.Redirect("/");
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

        public async Task<IActionResult> Delete(int id)
        {
            await this.newsService.DeleteAsync(id);

            return this.Redirect("/News/All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var viewModel = this.newsService.GetById<EditNewsViewModel>(id);
            var categories = this.categoriesService.GetCategories();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditNewsViewModel model)
        {
            await this.newsService.UpdateAsync(id, model);

            return this.Redirect("/News/All");
        }
    }
}
