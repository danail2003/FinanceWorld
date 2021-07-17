namespace FinanceWorld.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this.ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;

            return this.View("Error");
        }
    }
}
