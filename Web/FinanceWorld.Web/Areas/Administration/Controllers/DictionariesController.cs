namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DictionariesController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDictionariesService dictionariesService;

        public DictionariesController(
            UserManager<ApplicationUser> userManager,
            IDictionariesService dictionariesService)
        {
            this.userManager = userManager;
            this.dictionariesService = dictionariesService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
    }
}
