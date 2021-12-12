namespace FinanceWorld.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class AllUsersWithCoursesViewModel : IMapFrom<Course>
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
