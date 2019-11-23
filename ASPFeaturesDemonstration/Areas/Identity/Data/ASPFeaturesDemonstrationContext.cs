using ASPFeaturesDemonstration.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// This dbContext was placed in this folder automatically from scaffolding an Identity

namespace ASPFeaturesDemonstration.Models {
    public class ASPFeaturesDemonstrationContext : IdentityDbContext<ASPFeaturesDemonstrationUser> {
        public ASPFeaturesDemonstrationContext(DbContextOptions<ASPFeaturesDemonstrationContext> options)
            : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
