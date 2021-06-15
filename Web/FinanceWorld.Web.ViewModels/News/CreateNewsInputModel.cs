namespace FinanceWorld.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    public class CreateNewsInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }
    }
}
