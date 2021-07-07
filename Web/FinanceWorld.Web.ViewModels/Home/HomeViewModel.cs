namespace FinanceWorld.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using FinanceWorld.Web.ViewModels.Analyzes;
    using FinanceWorld.Web.ViewModels.News;

    public class HomeViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }

        public IEnumerable<AnalyzesByIdViewModel> Analyzes { get; set; }
    }
}
