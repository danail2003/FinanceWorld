namespace FinanceWorld.Web.ViewModels.Courses
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class CoursesViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
