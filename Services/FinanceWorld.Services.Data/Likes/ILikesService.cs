namespace FinanceWorld.Services.Data.Likes
{
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task SetLike(string analyzeId, string userId, int like);

        int GetLikes(string analyzeId);

        bool IsUserAlreadyLiked(string analyzeId, string userId);
    }
}
