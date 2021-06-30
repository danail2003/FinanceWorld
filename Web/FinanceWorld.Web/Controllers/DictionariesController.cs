namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.Dictionaries;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DictionariesController : BaseController
    {
        private readonly IDictionariesService dictionariesService;
        private readonly UserManager<ApplicationUser> userManager;

        public DictionariesController(IDictionariesService dictionariesService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.dictionariesService = dictionariesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDictionaryDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Error");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.dictionariesService.CreateAsync(dto, user.Id);

            return this.Redirect("/");
        }

        public IActionResult List()
        {
            var viewModel = new DictionaryListViewModel
            {
                Terms = this.dictionariesService.GetAll<TermViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var viewModel = this.dictionariesService.GetById<TermByIdViewModel>(id);

            return this.View(viewModel);
        }
    }
}
