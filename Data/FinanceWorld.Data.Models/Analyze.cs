namespace FinanceWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class Analyze : BaseDeletableModel<string>
    {
        public Analyze()
            => this.Id = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
