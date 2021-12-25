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

        public ICollection<UserCourse> UserCourses { get; set; } = new HashSet<UserCourse>();

        public ICollection<Lesson> Lessons { get; set; } = new HashSet<Lesson>();
    }
}
