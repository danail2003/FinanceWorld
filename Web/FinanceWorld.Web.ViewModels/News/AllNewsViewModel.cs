namespace FinanceWorld.Web.ViewModels.News
{
    using System.Collections.Generic;

    public class AllNewsViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }
    }
}
