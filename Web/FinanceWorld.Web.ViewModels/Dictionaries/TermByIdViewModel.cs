namespace FinanceWorld.Web.ViewModels.Dictionaries
{
    using FinanceWorld.Data.Models;
    using FinanceWorld.Services.Mapping;

    public class TermByIdViewModel : IMapFrom<Dictionary>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
