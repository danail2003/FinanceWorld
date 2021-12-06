namespace FinanceWorld.Services.Data.News
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.News;

    public interface INewsService
    {
        Task<News> CreateAsync(CreateNewsDto dto, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        IEnumerable<T> GetByCategory<T>(string name, int page, int itemsPerPage);

        T GetById<T>(int id);

        Task<News> UpdateAsync(int id, CreateEditNewsInputModel model);

        Task<int> DeleteAsync(int id);

        int GetCount();

        int GetCountByCategory(string name);

        bool IsNewsExist(int id);
    }
}
