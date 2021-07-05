namespace FinanceWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class Dictionary : BaseDeletableModel<string>
    {
        public Dictionary()
            => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
