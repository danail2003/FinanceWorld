namespace FinanceWorld.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;

    public interface ICoursesService
    {
        Task<Course> CreatAsync();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task<Course> UpdateAsync(int id);

        Task<int> DeleteAsync(int id);
    }
}
