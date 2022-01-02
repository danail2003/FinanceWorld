namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Services.Data.Evaluation;
    using FinanceWorld.Web.ViewModels.Evaluation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationsController : ControllerBase
    {
        private readonly IEvaluationService evaluationService;

        public EvaluationsController(IEvaluationService evaluationService)
            => this.evaluationService = evaluationService;

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<ActionResult<EvaluationViewModel>> Evaluate(EvaluationInputModel model)
        {
            if (model.Evaluation < 2 || model.Evaluation > 6)
            {
                return this.BadRequest();
            }

            await this.evaluationService.SetEvaluation(model.CourseId, model.AddedByUserId, model.Evaluation);

            double evaluation = this.evaluationService.GetEvaluation(model.CourseId, model.AddedByUserId);

            return new EvaluationViewModel { Evaluation = evaluation };
        }
    }
}
