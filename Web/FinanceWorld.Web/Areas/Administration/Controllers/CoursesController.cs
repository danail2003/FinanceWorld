namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Courses;
    using FinanceWorld.Services.Data.Models;
    using FinanceWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICoursesService coursesService,
            UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CourseDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
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
        public async Task<IActionResult> Edit(int id, CourseDto editDto)
        {
            CoursesViewModel course = this.coursesService.GetById<CoursesViewModel>(id);

            if (!this.ModelState.IsValid)
            {
                return this.View(course);
            }

            await this.coursesService.UpdateAsync(id, editDto);

            return this.RedirectToAction("All", "Courses", new { area = string.Empty });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.coursesService.DeleteAsync(id);

            return this.RedirectToAction("All", "Courses", new { area = string.Empty });
        }

        public async Task<IActionResult> AllUsersWithCourses()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            AllUsersWithCoursesViewModel view = new()
            {
                User = this.coursesService.GetAllUsersWithCourses<UsersWithCoursesViewModel>(),
                Courses = this.coursesService.GetAllCoursesWithUsers<MyCoursesViewModel>(user.Id),
            };

            return this.View(view);
        }
    }
}
