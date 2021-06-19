namespace FinanceWorld.Web.ViewModels.Dictionaries
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class TermViewModel : IMapFrom<Dictionary>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
