namespace FinanceWorld.Web.ViewModels.Courses
{
    using AutoMapper;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class MyCoursesViewModel : IMapFrom<UserCourse>, IHaveCustomMappings
    {
        public string AddedByUser { get; set; }

        public string Name { get; set; }

        public double Grade { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<UserCourse, MyCoursesViewModel>()
            .ForMember(x => x.Name, opt =>
              opt.MapFrom(x => x.Course.Name));
    }
}
