namespace FinanceWorld.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class AllUsersWithCoursesViewModel : IMapFrom<Course>
    {
        public List<UsersWithCoursesViewModel> User { get; set; }

        public List<MyCoursesViewModel> Courses { get; set; }
    }
}
