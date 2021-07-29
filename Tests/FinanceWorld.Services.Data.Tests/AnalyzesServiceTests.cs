namespace FinanceWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
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
            var imageContent = "Hello";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyzeWhenFileFormatIsJPEG()
        {
            var imageContent = "Hello";
            var fileName = "test.jpeg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyzeWhenFileFormatIsJPG()
        {
            var imageContent = "Hello";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddAnalyzeWhenFileFormatIsGIF()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.Single(this.analyzes);
        }

        [Fact]
        public async Task CreateMethodShouldAddManyAnalyzes()
        {
            var imageContent = "Hello";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dfsdsdfas", Title = "test", }, "2", "test");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "Test", Title = "test", }, "3", "test/test");

            Assert.Equal(3, this.analyzes.Count);
        }

        [Fact]
        public void CreateMethodShouldThrowExceptionIfFileExtensionIsNotValid()
        {
            var imageContent = "Hello";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
              {
                  await this.analyzesService.CreateAsync(
                  new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");
              });
        }

        [Fact]
        public async Task IsAnalyzeAndUserMatchShouldReturnTrueIfBothExist()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.True(this.analyzesService.IsAnalyzeAndUserMatch(id, "1"));
        }

        [Fact]
        public async Task IsAnalyzeAndUserMatchShouldReturnFalseIfOneOfTheDoesnotExist()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.False(this.analyzesService.IsAnalyzeAndUserMatch(null, "1"));
        }

        [Fact]
        public async Task GetCountMethodShouldReturnExactCount()
        {
            var imageContent = "Hello";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

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
            var imageContent = "Hello";
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dfsdsdfas", Title = "test", }, "2", "test");

            await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "Test", Title = "test", }, "3", "test/test");

            Assert.False(this.analyzesService.GetCount() == 2);
        }

        /*[Fact]
        public async Task DisplayAnalyzeInfoMethodShouldWorkCorrect()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            Assert.True(this.analyzesService.DisplayAnalyzeInfo(id).IsModified == null);
        }*/

        [Fact]
        public async Task DeleteAsyncMethodShouldDeleteAnalyze()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            await this.analyzesService.DeleteAsync(id);

            Assert.Empty(this.analyzes);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperlyWhenTitleIsChanged()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            var analyze = await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "adss" });

            Assert.Equal("adss", analyze.Title);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperlyWhenDescriptionIsChanged()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "dsaas", Title = "ads", }, "1", "das");

            var analyze = await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "ads" });

            Assert.Equal("test", analyze.Description);
        }

        [Fact]
        public async Task UpdateMethodShouldWorkProperlyWhenNothingIsChanged()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "test", Title = "ads", }, "1", "das");

            var analyze = await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "ads" });

            Assert.Equal("test", analyze.Description);
        }

        [Fact]
        public async Task UpdateMethodShoulThrowErrorWhenTitleIsTooLong()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "test", Title = "adsfdss", }, "1", "das");

            _ = Assert.ThrowsAsync<InvalidOperationException>(async () =>
              {
                  await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = "adshghhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh" });
              });
        }

        [Fact]
        public async Task UpdateMethodShoulThrowErrorWhenTitleIsEmpty()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "test", Title = "adsfdss", }, "1", "das");

            _ = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = "test", Title = null });
            });
        }

        [Fact]
        public async Task UpdateMethodShoulThrowErrorWhenDescriptionIsEmpty()
        {
            var imageContent = "Hello";
            var fileName = "test.gif";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(imageContent);
            writer.Flush();
            ms.Position = 0;
            this.fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            this.fileMock.Setup(x => x.FileName).Returns(fileName);
            this.fileMock.Setup(x => x.Length).Returns(ms.Length);
            var file = this.fileMock.Object;

            var id = await this.analyzesService.CreateAsync(
                new CreateAnalyzeInputModel { Image = file, Description = "test", Title = "adsfdss", }, "1", "das");

            _ = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await this.analyzesService.UpdateAsync(id, new EditAnalyzesViewModel { Description = null, Title = "adshghhhhhh" });
            });
        }
    }
}
