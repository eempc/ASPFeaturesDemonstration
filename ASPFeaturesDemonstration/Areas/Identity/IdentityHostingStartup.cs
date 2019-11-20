using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ASPFeaturesDemonstration.Areas.Identity.IdentityHostingStartup))]
namespace ASPFeaturesDemonstration.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            // The code below was moved to Startup.cs with builder and context removed
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<ASPFeaturesDemonstrationContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("ASPFeaturesDemonstrationContextConnection")));

            //    services.AddDefaultIdentity<ASPFeaturesDemonstrationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<ASPFeaturesDemonstrationContext>();
            //});
        }
    }
}