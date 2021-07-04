namespace FinanceWorld.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using FinanceWorld.Common;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Data.Dictionaries;
    using FinanceWorld.Services.Data.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
    }
}
