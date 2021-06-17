namespace FinanceWorld.Web.ViewModels.Analyzes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CreateAnalyzeInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
