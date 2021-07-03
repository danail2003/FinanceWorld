namespace FinanceWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FinanceWorld.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : Controller
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostLikeViewModel>> Like(LikeInputModel model)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;



            return new PostLikeViewModel { Like = 1 };
        }
    }
}
