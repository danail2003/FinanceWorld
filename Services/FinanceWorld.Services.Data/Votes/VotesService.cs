namespace FinanceWorld.Services.Data.Votes
{
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IDeletableEntityRepository<Vote> votesRepository;

        public VotesService(IDeletableEntityRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(string analyzeId)
        {
            return this.votesRepository.AllAsNoTracking().Where(x => x.AnalyzeId == analyzeId).Sum(x => (int)x.Type);
        }

        public async Task SetVote(string analyzeId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All().FirstOrDefault(x => x.AnalyzeId == analyzeId && x.AddedByUserId == userId);

            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.Like : VoteType.Dislike;
            }
            else
            {
                vote = new Vote
                {
                    AddedByUserId = userId,
                    AnalyzeId = analyzeId,
                    Type = isUpVote ? VoteType.Like : VoteType.Dislike,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
