﻿namespace FinanceWorld.Services.Data.News
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
            => this.newsRepository = newsRepository;

        public async Task CreateAsync(CreateNewsDto dto, string userId)
        {
            await this.newsRepository.AddAsync(new News
            {
                AddedByUserId = userId,
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
            });

            await this.newsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var news = this.newsRepository.All().FirstOrDefault(x => x.Id == id);

            this.newsRepository.Delete(news);

            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.newsRepository.AllAsNoTracking().OrderBy(x => x.CreatedOn).To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.newsRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
