namespace FinanceWorld.Services.Data.Votes
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVote(string analyzeId, string userId, bool isUpVote);

        int GetLikes(string analyzeId);

        int GetDislikes(string analyzeId);
    }
}
