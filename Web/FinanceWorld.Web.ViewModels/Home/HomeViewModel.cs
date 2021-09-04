namespace FinanceWorld.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using FinanceWorld.Web.ViewModels.Analyzes;
    using FinanceWorld.Web.ViewModels.News;

    public class HomeViewModel
    {
        public List<NewsViewModel> News { get; set; }

        public List<AnalysisViewModel> Analyzes { get; set; }
    }
}
