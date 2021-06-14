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

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new HashSet<IdentityUserRole<string>>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new HashSet<IdentityUserLogin<string>>();

        public virtual ICollection<News> News { get; set; } = new HashSet<News>();

        public virtual ICollection<Analyze> Analyzes { get; set; } = new HashSet<Analyze>();

        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public virtual ICollection<Dictionary> Dictionaries { get; set; } = new HashSet<Dictionary>();
    }
}
