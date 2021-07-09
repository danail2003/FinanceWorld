namespace FinanceWorld.Services.Data.News
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.News;

    public interface INewsService
    {
        Task CreateAsync(CreateNewsDto dto, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        Task UpdateAsync(int id, EditNewsViewModel model);

        Task DeleteAsync(int id);

        int GetCount();
    }
}
