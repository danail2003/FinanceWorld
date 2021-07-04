namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinanceWorld.Services.Data.Likes;
    using FinanceWorld.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : Controller
    {
        private readonly ILikesService likesService;

        public LikesController(ILikesService likesService)
        {
            this.likesService = likesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostLikeViewModel>> Like(LikeInputModel model)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (this.likesService.IsUserAlreadyLiked(model.AnalyzeId, userId))
            {
                return null;
            }

            //await this.likesService.SetLike(model.AnalyzeId, userId);

            return new PostLikeViewModel { Likes = this.likesService.GetLikes(model.AnalyzeId) };
        }
    }
}
