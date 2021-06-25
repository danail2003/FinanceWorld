namespace FinanceWorld.Web.ViewModels.News
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class NewsByIdViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }
    }
}
