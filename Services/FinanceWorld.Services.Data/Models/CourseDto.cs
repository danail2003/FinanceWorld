namespace FinanceWorld.Services.Data.Models
{
    using System.Collections.Generic;

    using FinanceWorld.Web.ViewModels.Courses;

    public class CourseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public ICollection<LessonsInputModel> Lessons { get; set; }
    }
}
