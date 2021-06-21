namespace FinanceWorld.Web.ViewModels.News
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class NewsViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string AddedByUserId { get; set; }
    }
}
