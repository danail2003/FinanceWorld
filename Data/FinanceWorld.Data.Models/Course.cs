namespace FinanceWorld.Data.Models
{
    using System.Collections.Generic;

    using FinanceWorld.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}
