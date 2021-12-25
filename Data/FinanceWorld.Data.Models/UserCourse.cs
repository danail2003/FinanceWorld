namespace FinanceWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserCourse
    {
        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [MaxLength(6)]
        public double Grade { get; set; }
    }
}
