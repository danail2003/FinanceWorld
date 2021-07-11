namespace FinanceWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Votes;
    using FinanceWorld.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostVoteViewModel>> Vote(VoteInputModel model)
        {
            var userId = await this.userManager.GetUserAsync(this.User);

            await this.votesService.SetVote(model.AnalyzeId, userId.Id, model.IsUpVote);
            var likes = this.votesService.GetLikes(model.AnalyzeId);
            var dislikes = this.votesService.GetDislikes(model.AnalyzeId);

            return new PostVoteViewModel { LikesCount = likes, DislikesCount = dislikes };
        }
    }
}
