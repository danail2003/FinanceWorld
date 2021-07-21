namespace FinanceWorld.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using AutoMapper;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;
    using FinanceWorld.Web.ViewModels.News;

    public class SearchByCategoriesViewModel : PaginationViewModel, IMapFrom<News>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public IEnumerable<NewsViewModel> News { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, NewsViewModel>()
                .ForMember(x => x.Title, opt =>
                opt.MapFrom(x => x.Name));
        }
    }
}
