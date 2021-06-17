namespace FinanceWorld.Services.Data.Analyzes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
