namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
    using Moq;
    using Xunit;

    public class DictionariesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Dictionary>> mockDictionary;
        private readonly List<Dictionary> dictionaries;
        private readonly DictionariesService dictionariesService;

        public DictionariesServiceTest()
        {
            this.mockDictionary = new Mock<IDeletableEntityRepository<Dictionary>>();
            this.dictionaries = new List<Dictionary>();
            this.mockDictionary.Setup(x => x.AllAsNoTracking()).Returns(this.dictionaries.AsQueryable());
            this.mockDictionary.Setup(x => x.AddAsync(It.IsAny<Dictionary>())).Callback((Dictionary dictionary) => this.dictionaries.Add(dictionary));
            this.dictionariesService = new DictionariesService(this.mockDictionary.Object);
        }

        [Fact]
        public async Task CreateMethodShouldWordCorrectly()
        {
            await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "Asset", Description = "sad" }, "1");

            Assert.Single(this.dictionaries);
        }

        [Fact]
        public async Task CreateMethodShouldWordCorrectlyWhenManyTermsWereAdded()
        {
            await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "Asset", Description = "sad" }, "1");

            await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "Liability", Description = "sadds" }, "2");

            await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "test", Description = "ss" }, "3");

            Assert.Equal(3, this.dictionaries.Count);
        }
    }
}
