namespace FinanceWorld.Web.ViewModels.News
{
    using System;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class NewsViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
