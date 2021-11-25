namespace FinanceWorld.Web.Controllers
{
    using System.Collections.Generic;

    using FinanceWorld.Services.Data.Courses;
    using FinanceWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult All()
        {
            IEnumerable<CoursesViewModel> allCourses = this.coursesService.GetAll<CoursesViewModel>();

            return this.View(allCourses);
        }

        public IActionResult ById(int id)
        {
            CoursesViewModel course = this.coursesService.GetById<CoursesViewModel>(id);

            return this.View(course);
        }
    }
}
