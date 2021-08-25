namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.Analyzes;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class AnalyzesServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly List<Analyze> analyzes;
        private readonly AnalyzesService analyzesService;
        private readonly Mock<IFormFile> fileMock;

        public AnalyzesServiceTests()
        {
            InitializeMapper();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.analyzes = new List<Analyze>();
            this.analyzesService = new AnalyzesService(this.mockAnalyzes.Object);
            this.mockAnalyzes.Setup(x => x.All()).Returns(this.analyzes.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
            this.mockAnalyzes.Setup(x => x.Delete(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Remove(analyze));
            this.fileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyze()
        {
            var file = this.InitializeFile("Hello", "test.png");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyzeWhenFileFormatIsJPEG()
        {
            var file = this.InitializeFile("Hello", "test.jpeg");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyzeWhenFileFormatIsJPG()
        {
            var file = this.InitializeFile("Hello", "test.jpg");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyzeWhenFileFormatIsGIF()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public void CreateMethodShouldThrowExceptionWhenFileFormatIsInvalid()
        {
            var file = this.InitializeFile("Hello", "test.doc");

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");
            });
        }

        [Fact]
        public async Task CreateMethodShouldAddManyAnalyzes()
        {
            var file = this.InitializeFile("Hello", "test.png");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dfsdsdfas", Title = "test", }, "2", "test");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "Test", Title = "test", }, "3", "test/test");

            Assert.Equal(3, this.analyzes.Count);
        }

        [Fact]
        public async Task IsAnalyzeAndUserMatchShouldReturnTrueIfBothExist()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.True(this.analyzesService.IsAnalyzeAndUserMatch(id, "1"));
        }

        [Fact]
        public async Task IsAnalyzeAndUserMatchShouldReturnFalseIfOneOfTheDoesnotExist()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.False(this.analyzesService.IsAnalyzeAndUserMatch("someId", "1"));
        }

        [Fact]
        public async Task GetCountMethodShouldReturnExactCount()
        {
            var file = this.InitializeFile("Hello", "test.png");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dfsdsdfas", Title = "test", }, "2", "test");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "Test", Title = "test", }, "3", "test/test");

            Assert.Equal(3, this.analyzesService.GetCount());
        }

        [Fact]
        public async Task GetCountMethodShouldReturnFalseIfCountDoesnotMatch()
        {
            var file = this.InitializeFile("Hello", "test.png");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dfsdsdfas", Title = "test", }, "2", "test");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "Test", Title = "test", }, "3", "test/test");

            Assert.False(this.analyzesService.GetCount() == 2);
        }

        [Fact]
        public async Task DeleteAsyncMethodShouldDeleteAnalyze()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.DeleteAsync(id);

            Assert.Empty(this.analyzes);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperlyWhenTitleIsChanged()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            var analyze = await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "adss" });

            Assert.Equal("adss", analyze.Title);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperlyWhenDescriptionIsChanged()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            var analyze = await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "ads" });

            Assert.Equal("test", analyze.Description);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperlyWhenNothingIsChanged()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "test", Title = "ads", }, "1", "das");

            var analyze = await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "ads" });

            Assert.Equal("test", analyze.Description);
        }

        [Fact]
        public async Task GetByIdShouldWorkProperly()
        {
            var file = this.InitializeFile("Hello", "test.gif");

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "test", Title = "ads", }, "1", "das");

            var result = this.analyzesService.GetById<AnalyzesByIdViewModel>(id);

            Assert.NotNull(result);
            Assert.Equal("ads", result.Title);
        }

        [Fact]
        public void GetAllShouldWorkProperly()
        {
            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "123",
                CreatedOn = DateTime.UtcNow,
                Id = "1",
                Image = new Image(),
                Description = "testtest2",
                Title = "test2",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "testtest",
                Title = "test",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            var result = this.analyzesService.GetAll<AnalyzesViewModel>(1, 8);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetMyAnalyzesShouldReturnCorrectCount()
        {
            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "123",
                CreatedOn = DateTime.UtcNow,
                Id = "1",
                Image = new Image(),
                Description = "testtest2",
                Title = "test2",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "testtest",
                Title = "test",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "testtest",
                Title = "test",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            var result = this.analyzesService.GetMyAnalyzes<AnalyzesViewModel>("12", 1, 8);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetMyAnalyzesShouldReturnOnlyMyAnalyzes()
        {
            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "1",
                Image = new Image(),
                Description = "testtest2",
                Title = "test2",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "testtest",
                Title = "test",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            var result = this.analyzesService.GetMyAnalyzes<AnalyzesViewModel>("12", 1, 8).ToList();

            Assert.Equal("testtest2", result[0].Description);
            Assert.Equal("testtest", result[1].Description);
        }

        [Fact]
        public void SearchedAnalyzesShouldReturnCorrectCount()
        {
            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "1",
                Image = new Image(),
                Description = "testtest2",
                Title = "test2",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "test",
                Title = "tipo",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            var result = this.analyzesService.SearchedAnalyzes<AnalyzesViewModel>("test", 1, 8);

            Assert.Single(result);
        }

        [Fact]
        public void SearchedAnalyzesShouldReturnCorrectAnalyzes()
        {
            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "1",
                Image = new Image(),
                Description = "testtest2",
                Title = "testsomething",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            this.analyzes.Add(new Analyze
            {
                AddedByUserId = "12",
                CreatedOn = DateTime.UtcNow,
                Id = "2",
                Image = new Image(),
                Description = "test",
                Title = "test",
                ImageId = "1",
                AddedByUser = new ApplicationUser(),
            });

            var result = this.analyzesService.SearchedAnalyzes<AnalyzesViewModel>("test", 1, 8).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("testsomething", result[0].Title);
            Assert.Equal("test", result[1].Title);
            Assert.Equal("/images/analyzes/", result[0].Image.Substring(0, 17));
            Assert.Equal(0, result[0].LikesCount);
            Assert.Equal(0, result[0].DislikesCount);
        }

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }

        private IFormFile InitializeFile(string imageContent, string fileName)
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);

            return this.fileMock.Object;
        }
    }
}
