namespace FinanceWorld.Web.ViewModels.Dictionaries
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDictionaryInputModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }
    }
}
