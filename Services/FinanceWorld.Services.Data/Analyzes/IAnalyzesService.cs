namespace FinanceWorld.Services.Data.Analyzes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Web.ViewModels.Analyzes;

    public interface IAnalyzesService
    {
        Task<string> CreateAsync(CreateAnalysisInputModel model, string userId, string path);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(string id);

        Task<Analyze> UpdateAsync(string id, EditAnalysisViewModel model);

        Task DeleteAsync(string id);

        IEnumerable<T> GetMyAnalyzes<T>(string userId, int page, int itemsPerPage);

        bool IsAnalyzeAndUserMatch(string id, string userId);

        int GetCount();

        IEnumerable<T> SearchedAnalyzes<T>(string title, int page, int itemsPerPage);
    }
}
