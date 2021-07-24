namespace FinanceWorld.Services.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateNewsDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
