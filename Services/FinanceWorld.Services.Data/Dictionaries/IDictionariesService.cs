namespace FinanceWorld.Services.Data.Dictionaries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Services.Data.Models;

    public interface IDictionariesService
    {
        Task CreateAsync(CreateDictionaryDto dto, string userId);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task UpdateAsync(int id);

        Task DeleteAsync(int id);
    }
}
