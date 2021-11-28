namespace FinanceWorld.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;

    public interface ICoursesService
    {
        Task<int> CreatAsync(CourseDto dto);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task<Course> UpdateAsync(int id, CourseDto dto);

        Task DeleteAsync(int id);

        Task Enroll(ApplicationUser user, int id);
    }
}
