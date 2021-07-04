namespace FinanceWorld.Services.Data.Likes
{
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;

    public class LikesService : ILikesService
    {
        private readonly IDeletableEntityRepository<Like> likesRepository;

        public LikesService(IDeletableEntityRepository<Like> likesRepository)
        {
            this.likesRepository = likesRepository;
        }

        public int GetLikes(string analyzeId)
        {
            return this.likesRepository.AllAsNoTracking().Count(x => x.AnalyzeId == analyzeId);
        }

        public bool IsUserAlreadyLiked(string analyzeId, string userId)
        {
            return this.likesRepository.AllAsNoTracking().Any(x => x.AnalyzeId == analyzeId && x.AddedByUserId == userId);
        }

        public async Task SetLike(string analyzeId, string userId, int like)
        {
            await this.likesRepository.AddAsync(new Like
            {
                AnalyzeId = analyzeId,
                AddedByUserId = userId,
            });

            await this.likesRepository.SaveChangesAsync();
        }
    }
}
