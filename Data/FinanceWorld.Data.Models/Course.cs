namespace FinanceWorld.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FinanceWorld.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public double Price { get; set; }

        public double Grade { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();

        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
    }
}
