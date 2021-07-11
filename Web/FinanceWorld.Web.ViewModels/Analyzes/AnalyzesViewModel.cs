﻿namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using System.Linq;

    using AutoMapper;
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class AnalyzesViewModel : IMapFrom<Analyze>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AddedByUserUserName { get; set; }

        public string Image { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Analyze, AnalyzesViewModel>()
                .ForMember(x => x.Image, opt =>
                opt.MapFrom(x => "/images/analyzes/" + x.Image.Id + "." + x.Image.Extension))
                .ForMember(x => x.LikesCount, opt =>
                opt.MapFrom(x => x.Votes.Count(v => (int)v.Type == 1)))
                .ForMember(x => x.DislikesCount, opt =>
                opt.MapFrom(x => x.Votes.Count(v => (int)v.Type == -1)));
        }
    }
}
