namespace FinanceWorld.Services.Data.Dictionaries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Services.Data.Models;

    public interface IDictionariesService
    {
        Task<string> CreateAsync(CreateDictionaryDto dto, string userId);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);

        bool IsTermExist(string id);
    }
}
