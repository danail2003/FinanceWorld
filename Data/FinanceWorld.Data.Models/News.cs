namespace FinanceWorld.Data.Models
{
    using FinanceWorld.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Content { get; set; }

        public virtual Image Image { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
