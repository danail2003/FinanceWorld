namespace FinanceWorld.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateNewsDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
