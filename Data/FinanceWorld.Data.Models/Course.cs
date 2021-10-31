namespace FinanceWorld.Data.Models
{
    using FinanceWorld.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
