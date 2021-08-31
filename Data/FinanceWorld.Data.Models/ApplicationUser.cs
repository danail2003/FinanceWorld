// ReSharper disable VirtualMemberCallInConstructor
namespace FinanceWorld.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FinanceWorld.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
           => this.Id = Guid.NewGuid().ToString();

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; } = new HashSet<IdentityUserRole<string>>();

        public ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        public ICollection<IdentityUserLogin<string>> Logins { get; set; } = new HashSet<IdentityUserLogin<string>>();

        public ICollection<News> News { get; set; } = new HashSet<News>();

        public ICollection<Analyze> Analyzes { get; set; } = new HashSet<Analyze>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public ICollection<Dictionary> Dictionaries { get; set; } = new HashSet<Dictionary>();

        public ICollection<Vote> Votes { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
