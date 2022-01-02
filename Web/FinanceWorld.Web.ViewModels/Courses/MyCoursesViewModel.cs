namespace FinanceWorld.Web.ViewModels.Courses
{
    using AutoMapper;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class MyCoursesViewModel : IMapFrom<UserCourse>, IMapFrom<Course>, IHaveCustomMappings
    {
        public string AddedByUserId { get; set; }

        public int CourseId { get; set; }

        public string AddedByUser { get; set; }

        public string CourseName { get; set; }

        public double Grade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Course, MyCoursesViewModel>()
            .ForMember(x => x.CourseName, opt =>
              opt.MapFrom(x => x.Name));
    }
}
