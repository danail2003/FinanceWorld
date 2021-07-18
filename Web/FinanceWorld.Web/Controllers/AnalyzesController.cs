namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyzesController : Controller
    {
        private readonly IAnalyzesService analyzesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public AnalyzesController(
            IAnalyzesService analyzesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.analyzesService = analyzesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnalyzeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Error");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.analyzesService.CreateAsync(model, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            if (id < 1)
            {
                throw new InvalidOperationException("The pages starts from one!");
            }

            const int itemsPerPage = 10;

            var viewModel = new AllAnalyzesViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Count = this.analyzesService.GetCount(),
                Analyzes = this.analyzesService.GetAll<AnalyzesViewModel>(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.analyzesService.DeleteAsync(id);

            return this.Redirect("/Analyzes/All");
        }

        [Authorize]
        public async Task<IActionResult> AnalyzesById()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AllAnalyzesViewModel
            {
                Analyzes = this.analyzesService.GetMyAnalyzes<AnalyzesViewModel>(user.Id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.analyzesService.IsAnalyzeAndUserMatch(id, user.Id))
            {
                throw new InvalidOperationException("This analyze doesn't belong to that user!");
            }

            var viewModel = this.analyzesService.GetById<AnalyzesByIdViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditAnalyzesViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.analyzesService.IsAnalyzeAndUserMatch(id, user.Id))
            {
                throw new InvalidOperationException("This analyze doesn't belong to that user!");
            }

            await this.analyzesService.UpdateAsync(id, model);

            return this.Redirect("/Analyzes/All");
        }

        public IActionResult ById(string id)
        {
            var viewModel = new AnalyzeWithInfoViewModel
            {
                Analyze = this.analyzesService.GetById<AnalyzesViewModel>(id),
                AnalyzeWithInfo = this.analyzesService.DisplayAnalyzeInfo(id),
            };

            return this.View(viewModel);
        }

        public IActionResult SearchAnalyze([FromQuery] AllAnalyzesViewModel model)
        {
            var viewModel = new AllAnalyzesViewModel
            {
                Analyzes = this.analyzesService.SearchedAnalyzes<AnalyzesViewModel>(model.SearchTitle),
            };

            return this.View(viewModel);
        }
    }
}
