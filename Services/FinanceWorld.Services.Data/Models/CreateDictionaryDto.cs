namespace FinanceWorld.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDictionaryDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }
    }
}
