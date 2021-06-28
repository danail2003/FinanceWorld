namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class AnalyzesByIdViewModel : IMapFrom<Analyze>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
