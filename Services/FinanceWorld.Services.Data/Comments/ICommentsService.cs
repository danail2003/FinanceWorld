namespace FinanceWorld.Services.Data.Comments
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(string analyzeId, string userId, string content, string parentId);

        bool IsInAnalyzeId(string commentId, string analyzeId);
    }
}
