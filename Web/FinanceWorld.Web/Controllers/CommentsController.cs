namespace FinanceWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Comments;
    using FinanceWorld.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);

            await this.commentsService.Create(inputModel.AnalyzeId, userId, inputModel.Content);

            return this.RedirectToAction("ById", "Analyzes", new { id = inputModel.AnalyzeId });
        }
    }
}
