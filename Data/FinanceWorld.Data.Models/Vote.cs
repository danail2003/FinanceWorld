namespace FinanceWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FinanceWorld.Data.Common.Models;

    public class Vote : BaseDeletableModel<int>
    {
        [Required]
        public string AnalyzeId { get; set; }

        public Analyze Analyze { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public VoteType Type { get; set; }
    }
}
