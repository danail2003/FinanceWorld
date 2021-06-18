namespace FinanceWorld.Services.Data.Models
{
    using System.Collections.Generic;

    using AutoMapper;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class DictionaryListDto : IMapFrom<Dictionary>, IHaveCustomMappings
    {
        public IEnumerable<TermDto> Terms { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Dictionary, DictionaryListDto>()
                .ForMember(x => x.Terms, opt =>
                opt.MapFrom(x => x.Name));
        }
    }
}
