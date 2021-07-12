namespace FinanceWorld.Web.ViewModels
{
    using System;

    public class PaginationViewModel
    {
        public int PageNumber { get; set; }

        public int Count { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPage => this.PageNumber + 1;

        public int PreviousPage => this.PageNumber - 1;

        public int PagesCount => (int)Math.Ceiling((double)this.Count / this.ItemsPerPage);
    }
}
