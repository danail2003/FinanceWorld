namespace FinanceWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
            => this.Id = Guid.NewGuid().ToString();

        public string Extension { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(Analyze))]
        public string AnalyzeId { get; set; }

        public Analyze Analyze { get; set; }
    }
}
