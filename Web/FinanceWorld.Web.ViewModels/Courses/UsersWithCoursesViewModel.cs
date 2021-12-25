namespace FinanceWorld.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class UsersWithCoursesViewModel : IMapFrom<ApplicationUser>
    {
        public IEnumerable<MyCoursesViewModel> Courses { get; set; }
    }
}
