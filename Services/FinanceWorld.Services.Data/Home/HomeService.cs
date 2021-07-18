namespace FinanceWorld.Services.Data.Home
{
    using System.Collections.Generic;
    using System.Linq;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;
        private readonly IDeletableEntityRepository<Analyze> analyzesRepository;

        public HomeService(
            IDeletableEntityRepository<News> newsRepository,
            IDeletableEntityRepository<Analyze> analyzesRepository)
        {
            this.newsRepository = newsRepository;
            this.analyzesRepository = analyzesRepository;
        }

        public List<T> GetLastThreeAnalyzes<T>()
        {
            return this.analyzesRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).Take(3).To<T>().ToList();
        }

        public List<T> GetLastThreeNews<T>()
        {
            return this.newsRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).Take(3).To<T>().ToList();
        }
    }
}
