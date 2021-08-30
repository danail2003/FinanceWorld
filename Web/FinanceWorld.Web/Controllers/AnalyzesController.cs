namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyzesController : Controller
    {
        private const int ItemsPerPage = 10;
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

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnalyzeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
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

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully added analyze!";

            return this.RedirectToAction(nameof(HomeController.Index), "/");
        }

        public IActionResult All(int id = 1)
        {
            if (id < 1)
            {
                throw new InvalidOperationException("The pages starts from one!");
            }

            var viewModel = new AllAnalyzesViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.analyzesService.GetCount(),
                Analyzes = this.analyzesService.GetAll<AnalyzesViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.analyzesService.DeleteAsync(id);

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully delete!";

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> AnalyzesById(int id = 1)
        {
            if (id < 1)
            {
                throw new InvalidOperationException("The pages starts from one!");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AllAnalyzesViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.analyzesService.GetCount(),
                Analyzes = this.analyzesService.GetMyAnalyzes<AnalyzesViewModel>(user.Id, id, ItemsPerPage),
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

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully edited analyze!";

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult ById(string id)
        {
            var viewModel = this.analyzesService.GetById<AnalyzesViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult SearchAnalyze([FromQuery] AllAnalyzesViewModel model, int id = 1)
        {
            if (id < 1)
            {
                throw new InvalidOperationException("The pages starts from one!");
            }

            var viewModel = new AllAnalyzesViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Count = this.analyzesService.GetCount(),
                Analyzes = this.analyzesService.SearchedAnalyzes<AnalyzesViewModel>(model.SearchTitle, id, ItemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}
