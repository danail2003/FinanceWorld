namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditAnalyzesViewModel : IMapFrom<Analyze>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
