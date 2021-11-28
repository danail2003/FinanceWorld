namespace FinanceWorld.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    public class CourseInputModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Range(1, 5000)]
        public double Price { get; set; }
    }
}
