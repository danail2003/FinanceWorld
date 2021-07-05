namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinanceWorld.Services.Data.Votes;
    using FinanceWorld.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteViewModel>> Vote(VoteInputModel model)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.votesService.SetVote(model.AnalyzeId, userId, model.IsUpVote);
            var votes = this.votesService.GetVotes(model.AnalyzeId);

            return new PostVoteViewModel { Likes = votes };
        }
    }
}
