namespace FinanceWorld.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FinanceWorld.Data.Common.Models;

    public class Lesson : BaseDeletableModel<string>
    {
        public Lesson()
            => this.Id = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
