namespace FinanceWorld.Web.Controllers
{
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Web.ViewModels.Dictionaries;
    using Microsoft.AspNetCore.Mvc;

    public class DictionariesController : Controller
    {
        private readonly IDictionariesService dictionariesService;

        public DictionariesController(IDictionariesService dictionariesService)
            => this.dictionariesService = dictionariesService;

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
            if (!this.dictionariesService.IsTermExist(id))
            {
                return this.BadRequest();
            }

            TermByIdViewModel viewModel = this.dictionariesService.GetById<TermByIdViewModel>(id);

            return this.View(viewModel);
        }
    }
}
