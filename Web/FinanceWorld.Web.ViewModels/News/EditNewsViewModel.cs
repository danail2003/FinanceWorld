namespace FinanceWorld.Web.ViewModels.News
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class EditNewsViewModel : CreateNewsInputModel, IMapFrom<News>
    {
        public string Id { get; set; }
    }
}
