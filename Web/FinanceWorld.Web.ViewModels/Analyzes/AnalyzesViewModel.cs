namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using AutoMapper;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class AnalyzesViewModel : IMapFrom<Analyze>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string AddedByUserUserName { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Analyze, AnalyzesViewModel>()
                .ForMember(x => x.Image, opt =>
                opt.MapFrom(x => "/images/analyzes/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
