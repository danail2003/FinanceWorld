namespace FinanceWorld.Services.Data.Dictionaries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDictionariesService
    {
        Task CreateAsync<T>(T model, string userId, string path);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task UpdateAsync(int id);

        Task DeleteAsync(int id);
    }
}
