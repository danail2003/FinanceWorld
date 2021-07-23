namespace FinanceWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
