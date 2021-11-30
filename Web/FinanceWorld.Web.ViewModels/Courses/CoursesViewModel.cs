namespace FinanceWorld.Web.ViewModels.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class CoursesViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Range(1, 5000)]
        public double Price { get; set; }

        public ICollection<LessonsInputModel> Lessons { get; set; }
    }
}
