namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllAnalyzesViewModel : PaginationViewModel
    {
        [Display(Name = "Search by title")]
        public string SearchTitle { get; set; }

        public IEnumerable<AnalyzesViewModel> Analyzes { get; set; }
    }
}
