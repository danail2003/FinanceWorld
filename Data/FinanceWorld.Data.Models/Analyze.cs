namespace FinanceWorld.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class Analyze : BaseDeletableModel<string>
    {
        public Analyze()
            => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(Image))]
        public string ImageId { get; set; }

        public Image Image { get; set; }

        public ICollection<Vote> Votes { get; set; } = new HashSet<Vote>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
