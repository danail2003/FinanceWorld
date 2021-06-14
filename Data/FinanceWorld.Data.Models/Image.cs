namespace FinanceWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
            => this.Id = Guid.NewGuid().ToString();

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public string ImageUrl { get; set; }

        public string Extension { get; set; }

        public int NewsId { get; set; }

        public virtual News News { get; set; }

        [ForeignKey(nameof(Analyze))]
        public string AnalyzeId { get; set; }

        public virtual Analyze Analyze { get; set; }
    }
}
