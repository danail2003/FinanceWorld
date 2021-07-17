namespace FinanceWorld.Web.Controllers
{
    using System;

    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Web.ViewModels.Dictionaries;
    using Microsoft.AspNetCore.Mvc;

    public class DictionariesController : Controller
    {
        private readonly IDictionariesService dictionariesService;

        public DictionariesController(IDictionariesService dictionariesService)
        {
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
            TermByIdViewModel viewModel;

            try
            {
                viewModel = this.dictionariesService.GetById<TermByIdViewModel>(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return this.View(viewModel);
        }
    }
}
