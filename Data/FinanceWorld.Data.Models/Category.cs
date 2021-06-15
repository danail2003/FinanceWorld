namespace FinanceWorld.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FinanceWorld.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; } = new HashSet<News>();
    }
}
