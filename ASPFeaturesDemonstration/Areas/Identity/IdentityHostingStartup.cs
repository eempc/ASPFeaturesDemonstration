using System;
using ASPFeaturesDemonstration.Areas.Identity.Data;
using ASPFeaturesDemonstration.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ASPFeaturesDemonstration.Areas.Identity.IdentityHostingStartup))]
namespace ASPFeaturesDemonstration.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
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