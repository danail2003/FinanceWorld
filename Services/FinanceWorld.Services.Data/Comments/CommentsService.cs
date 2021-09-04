namespace FinanceWorld.Services.Data.Comments
{
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(string analyzeId, string userId, string content)
        {
            await this.commentsRepository.AddAsync(new Comment
            {
                AddedByUserId = userId,
                AnalyzeId = analyzeId,
                Content = content,
            });

            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInAnalyzeId(string commentId, string analyzeId)
        {
            var commentAnalyzeId = this.commentsRepository.All()
                .Where(x => x.Id == commentId).Select(x => x.AnalyzeId).FirstOrDefault();

            return commentAnalyzeId == analyzeId;
        }
    }
}
