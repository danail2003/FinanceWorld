namespace FinanceWorld.Services.Data.Analyzes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Analyzes;

    public class AnalyzesService : IAnalyzesService
    {
        private readonly string[] allowedExtensions = new[] { "jpeg", "png", "jpg", "gif" };
        private readonly IDeletableEntityRepository<Analyze> analyzesRepository;

        public AnalyzesService(IDeletableEntityRepository<Analyze> analyzesRepository)
            => this.analyzesRepository = analyzesRepository;

        public async Task CreateAsync(CreateAnalyzeInputModel model, string userId, string path)
        {
            var analyze = new Analyze
            {
                Title = model.Title,
                Description = model.Description,
                AddedByUserId = userId,
            };

            Directory.CreateDirectory($"{path}/analyzes/");

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new InvalidOperationException("File format is not allowed");
            }

            var image = new Image
            {
                Extension = extension,
                AddedByUserId = userId,
            };

            analyze.Image = image;

            var physicalPath = $"{path}/analyzes/{image.Id}.{extension}";
            using Stream stream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(stream);

            await this.analyzesRepository.AddAsync(analyze);
            await this.analyzesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var analyze = this.analyzesRepository.All().FirstOrDefault(x => x.Id == id);

            this.analyzesRepository.Delete(analyze);

            await this.analyzesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.analyzesRepository.AllAsNoTracking().Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            return this.analyzesRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public int GetCount()
        {
            return this.analyzesRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetMyAnalyzes<T>(string userId)
        {
            return this.analyzesRepository.All().Where(x => x.AddedByUserId == userId).To<T>().ToList();
        }

        public bool IsAnalyzeAndUserMatch(string id, string userId)
        {
            return this.analyzesRepository.AllAsNoTracking().Any(x => x.AddedByUserId == userId && x.Id == id);
        }

        public async Task UpdateAsync(string id, EditAnalyzesViewModel model)
        {
            var analyze = this.analyzesRepository.All().FirstOrDefault(x => x.Id == id);

            analyze.Title = model.Title;
            analyze.Description = model.Description;

            await this.analyzesRepository.SaveChangesAsync();
        }
    }
}
