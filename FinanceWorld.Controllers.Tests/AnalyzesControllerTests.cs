namespace FinanceWorld.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Analyzes;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Services.Messaging;
    using FinanceWorld.Web.Controllers;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    public class AnalyzesControllerTests
    {
        private readonly Mock<IDeletableEntityRepository<Analyze>> mockAnalyzes;
        private readonly List<Analyze> analyzes;
        private readonly AnalyzesService analyzesService;
        private readonly Mock<IFormFile> fileMock;
        private readonly Mock<UserManager<ApplicationUser>> userManager;
        private readonly Mock<IWebHostEnvironment> environment;
        private readonly Mock<IEmailSender> emailSender;

        public AnalyzesControllerTests()
        {
            InitializeMapper();
            this.mockAnalyzes = new Mock<IDeletableEntityRepository<Analyze>>();
            this.analyzes = new List<Analyze>();
            this.fileMock = new Mock<IFormFile>();
            this.userManager = new Mock<UserManager<ApplicationUser>>();
            this.environment = new Mock<IWebHostEnvironment>();
            this.emailSender = new Mock<IEmailSender>();
            this.analyzesService = new AnalyzesService(this.mockAnalyzes.Object);
            this.mockAnalyzes.Setup(x => x.AllAsNoTracking()).Returns(this.analyzes.AsQueryable());
            this.mockAnalyzes.Setup(x => x.AddAsync(It.IsAny<Analyze>())).Callback((Analyze analyze) => this.analyzes.Add(analyze));
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

        private static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("FinanceWorld.Web.ViewModels"));
        }
    }
}
