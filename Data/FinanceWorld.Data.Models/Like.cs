namespace FinanceWorld.Data.Models
{
    using FinanceWorld.Data.Common.Models;

    public class Like : BaseDeletableModel<int>
    {
        public string AnalyzeId { get; set; }

        public Analyze Analyze { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
