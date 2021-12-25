namespace FinanceWorld.Web.ViewModels.Courses
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class MyCoursesViewModel : IMapFrom<Course>, IMapFrom<UserCourse>
    {
        public string AddedByUser { get; set; }

        public string Name { get; set; }

        public double Grade { get; set; }
    }
}
