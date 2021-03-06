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

        IEnumerable<T> GetMyCourses<T>(string userId);

        T GetById<T>(int id);

        Task<Course> UpdateAsync(int id, CourseDto dto);

        Task DeleteAsync(int id);

        Task Enroll(ApplicationUser user, int id);

        IEnumerable<string> GetUsersIds();

        List<T> GetAllCoursesWithUsers<T>(IEnumerable<int> courses, IEnumerable<string> users);

        IEnumerable<int> GetCoursesIds();
    }
}
