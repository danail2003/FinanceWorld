namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using System.ComponentModel.DataAnnotations;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class EditAnalysisViewModel : IMapFrom<Analyze>
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
