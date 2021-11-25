namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Services.Data.Courses;
    using FinanceWorld.Services.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
            => this.coursesService = coursesService;

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            await this.coursesService.CreatAsync(dto);

            return this.RedirectToAction("All", "Courses", new { area = string.Empty });
        }
    }
}
