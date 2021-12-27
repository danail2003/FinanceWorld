namespace FinanceWorld.Services.Data.Evaluation
{
    using System.Linq;
    using System.Threading.Tasks;

    using FinanceWorld.Data.Common.Repositories;
    using FinanceWorld.Data.Models;

    public class EvaluationService : IEvaluationService
    {
        private readonly IRepository<UserCourse> usersCoursesRepository;

        public EvaluationService(IRepository<UserCourse> usersCoursesRepository)
            => this.usersCoursesRepository = usersCoursesRepository;

        public double GetEvaluation(int courseId, string userId)
            => this.usersCoursesRepository.AllAsNoTracking().First(x => x.CourseId == courseId && x.AddedByUserId == userId).Grade;

        public async Task SetEvaluation(int courseId, string userId, double evaluation)
        {
            UserCourse course = this.usersCoursesRepository.All().FirstOrDefault(x => x.CourseId == courseId && x.AddedByUserId == userId);

            course.Grade = evaluation;

            await this.usersCoursesRepository.SaveChangesAsync();
        }
    }
}
