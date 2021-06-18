namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
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
            var user = await this.userManager.GetUserAsync(this.User);

            await this.dictionariesService.CreateAsync(dto, user.Id);

            return this.Redirect("/");
        }

        public IActionResult List()
        {
            var dictionary = this.dictionariesService.GetAll<DictionaryListDto>();

            return this.View(dictionary);
        }
    }
}
