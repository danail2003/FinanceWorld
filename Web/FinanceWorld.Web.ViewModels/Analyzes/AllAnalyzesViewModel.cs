namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using System.Collections.Generic;

    public class AllAnalyzesViewModel : PaginationViewModel
    {
        public IEnumerable<AnalyzesViewModel> Analyzes { get; set; }
    }
}
