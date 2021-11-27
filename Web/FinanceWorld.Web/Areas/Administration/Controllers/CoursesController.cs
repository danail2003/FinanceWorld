namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Services.Data.Courses;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
            => this.coursesService = coursesService;

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct!");
            }

            await this.coursesService.CreatAsync(dto);

            return this.RedirectToAction("All", "Courses", new { area = string.Empty });
        }

        public IActionResult Edit(int id)
        {
            CoursesViewModel course = this.coursesService.GetById<CoursesViewModel>(id);

            return this.View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCoursesDto editDto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidOperationException("Data is not correct");
            }

            await this.coursesService.UpdateAsync(id, editDto);

            return this.RedirectToAction("All", "Courses", new { area = string.Empty });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.coursesService.DeleteAsync(id);

            return this.RedirectToAction("All", "Courses", new { area = string.Empty });
        }
    }
}
