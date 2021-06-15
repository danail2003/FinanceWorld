namespace FinanceWorld.Services.Data.News
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Services.Data.Models;

    public interface INewsService
    {
        Task CreateAsync(CreateNewsDto dto, string userId, string path);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task UpdateAsync(int id);

        Task DeleteAsync(int id);
    }
}
