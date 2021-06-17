namespace FinanceWorld.Services.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDictionaryDto
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
