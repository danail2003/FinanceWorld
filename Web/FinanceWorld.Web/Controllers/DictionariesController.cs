namespace FinanceWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.Dictionaries;
    using Microsoft.AspNetCore.Authorization;
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
