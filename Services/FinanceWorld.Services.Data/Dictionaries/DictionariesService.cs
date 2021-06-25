﻿namespace FinanceWorld.Services.Data.Dictionaries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class DictionariesService : IDictionariesService
    {
        private readonly IDeletableEntityRepository<Dictionary> dictionaryRepository;

        public DictionariesService(IDeletableEntityRepository<Dictionary> dictionaryRepository)
            => this.dictionaryRepository = dictionaryRepository;

        public async Task CreateAsync(CreateDictionaryDto dto, string userId)
        {
            await this.dictionaryRepository.AddAsync(new Dictionary
            {
                Name = dto.Name,
                Description = dto.Description,
                AddedByUserId = userId,
            });

            await this.dictionaryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.dictionaryRepository.AllAsNoTracking().OrderBy(x => x.Name).To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            return this.dictionaryRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }
    }
}
