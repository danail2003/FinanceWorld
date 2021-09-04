namespace FinanceWorld.Data.Models
{
    using System;

    using FinanceWorld.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
            => this.Id = Guid.NewGuid().ToString();

        public string Content { get; set; }

        public string AnalyzeId { get; set; }

        public Analyze Analyze { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
