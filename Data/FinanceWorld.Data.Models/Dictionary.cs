namespace FinanceWorld.Data.Models
{
    using System;

    using FinanceWorld.Data.Common.Models;

    public class Dictionary : BaseDeletableModel<string>
    {
        public Dictionary()
            => this.Id = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
