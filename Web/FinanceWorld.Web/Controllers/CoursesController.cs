namespace FinanceWorld.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Courses;
    using FinanceWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            IEnumerable<CoursesViewModel> allCourses = this.coursesService.GetAll<CoursesViewModel>();

            return this.View(allCourses);
        }

        public async Task<IActionResult> MyCourses()
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.BadRequest();
            }

            IEnumerable<MyCoursesViewModel> viewModel = this.coursesService.GetMyCourses<MyCoursesViewModel>(user.Id);

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            CoursesViewModel course = this.coursesService.GetById<CoursesViewModel>(id);

            return this.View(course);
        }

        public async Task<IActionResult> Enroll(int id)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.BadRequest();
            }

            await this.coursesService.Enroll(user, id);

            return this.RedirectToAction("MyCourses", "Courses");
        }
    }
}
