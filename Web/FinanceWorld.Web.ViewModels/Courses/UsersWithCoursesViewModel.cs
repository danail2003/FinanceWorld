namespace FinanceWorld.Web.ViewModels.Courses
{
    using AutoMapper;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class UsersWithCoursesViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string AddedByUser { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<ApplicationUser, UsersWithCoursesViewModel>()
            .ForMember(x => x.AddedByUser, opt =>
              opt.MapFrom(u => u.UserName));
    }
}
