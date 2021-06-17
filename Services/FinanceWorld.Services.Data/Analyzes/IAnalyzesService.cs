namespace FinanceWorld.Services.Data.Analyzes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Web.ViewModels.Analyzes;

    public interface IAnalyzesService
    {
        Task CreateAsync(CreateAnalyzeInputModel model, string userId, string path);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task UpdateAsync(int id);

        Task DeleteAsync(int id);
    }
}
