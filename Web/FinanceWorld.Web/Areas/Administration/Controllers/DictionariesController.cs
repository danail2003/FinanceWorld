namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DictionariesController : Controller
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

        public IActionResult Create()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateDictionaryDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.dictionariesService.CreateAsync(dto, user.Id);

            this.TempData[GlobalConstants.GlobalMessage] = "Successfully added term!";

            return this.Redirect("/");
        }
    }
}
