namespace FinanceWorld.Data.Models
{
    using System.Collections.Generic;

    using FinanceWorld.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
    }
}
