namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Dictionaries;
    using Moq;
    using Xunit;

    public class DictionariesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Dictionary>> mockDictionary;
        private readonly List<Dictionary> dictionaries;
        private readonly DictionariesService dictionariesService;

        public DictionariesServiceTest()
        {
            InitializeMapper();
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

        [Fact]
        public async Task GetByIdShouldWorkCorrectly()
        {
            var id = await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "Asset", Description = "sad" }, "1");

            var result = this.dictionariesService.GetById<TermByIdViewModel>(id);

            Assert.Equal("Asset", result.Name);
            Assert.Equal("sad", result.Description);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectCount()
        {
            await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "Asset", Description = "sad" }, "1");
            await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "test", Description = "test" }, "1");

            var result = this.dictionariesService.GetAll<TermViewModel>();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectValues()
        {
            var firstId = await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "Asset", Description = "sad" }, "1");
            var secondId = await this.dictionariesService.CreateAsync(new CreateDictionaryDto { Name = "test", Description = "test" }, "1");

            var result = this.dictionariesService.GetAll<TermViewModel>().ToList();

            Assert.Equal("Asset", result[0].Name);
            Assert.Equal("test", result[1].Name);
            Assert.Equal(firstId, result[0].Id);
            Assert.Equal(secondId, result[1].Id);
        }

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }
    }
}
