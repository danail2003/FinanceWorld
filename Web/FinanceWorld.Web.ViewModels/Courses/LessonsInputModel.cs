namespace FinanceWorld.Web.ViewModels.Courses
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class LessonsInputModel : IMapFrom<Lesson>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
