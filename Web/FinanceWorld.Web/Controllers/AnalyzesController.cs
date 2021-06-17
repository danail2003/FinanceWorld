namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyzesController : BaseController
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
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.analyzesService.CreateAsync(model, user.Id, $"{this.environment.WebRootPath}/analyzes");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            return this.Redirect("/");
        }
    }
}
