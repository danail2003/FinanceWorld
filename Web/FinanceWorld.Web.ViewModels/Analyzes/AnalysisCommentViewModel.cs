namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using System;

    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;
    using Ganss.XSS;

    public class AnalysisCommentViewModel : IMapFrom<Comment>
    {
        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string AddedByUser { get; set; }
    }
}
