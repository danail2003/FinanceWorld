namespace FinanceWorld.Web.ViewModels.Dictionaries
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDictionaryInputModel
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
