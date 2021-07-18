namespace FinanceWorld.Services.Data.Analyzes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Web.ViewModels.Analyzes;

    public interface IAnalyzesService
    {
        Task CreateAsync(CreateAnalyzeInputModel model, string userId, string path);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(string id);

        Task UpdateAsync(string id, EditAnalyzesViewModel model);

        Task DeleteAsync(string id);

        IEnumerable<T> GetMyAnalyzes<T>(string userId);

        bool IsAnalyzeAndUserMatch(string id, string userId);

        AnalyzeInfoViewModel DisplayAnalyzeInfo(string id);

        int GetCount();

        IEnumerable<T> SearchedAnalyzes<T>(string title);
    }
}
