﻿namespace FinanceWorld.Services.Data.News
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.News;

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

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.newsRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
        }

        public IEnumerable<T> GetByCategory<T>(string name, int page, int itemsPerPage)
        {
            return this.newsRepository.All().OrderByDescending(x => x.CreatedOn).Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).Where(x => x.Category.Name.ToLower().Contains(name)).To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.newsRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public int GetCount()
        {
            return this.newsRepository.AllAsNoTracking().Count();
        }

        public async Task UpdateAsync(int id, EditNewsViewModel model)
        {
            var news = this.newsRepository.All().FirstOrDefault(x => x.Id == id);

            news.ImageUrl = model.ImageUrl;
            news.Title = model.Title;
            news.Content = model.Content;
            news.CategoryId = model.CategoryId;

            await this.newsRepository.SaveChangesAsync();
        }
    }
}
