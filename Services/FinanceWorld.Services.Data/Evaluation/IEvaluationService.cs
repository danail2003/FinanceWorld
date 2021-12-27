namespace FinanceWorld.Services.Data.Evaluation
{
    using System.Threading.Tasks;

    public interface IEvaluationService
    {
        Task SetEvaluation(int courseId, string userId, double evaluation);

        double GetEvaluation(int courseId, string userId);
    }
}
