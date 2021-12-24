namespace FinanceWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBase
    {
        Task<string> CreateAsync<T>(T model, string userId, string path);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(string id);

        Task<T> UpdateAsync<T>(string id, T model);

        Task DeleteAsync(string id);
    }
}
