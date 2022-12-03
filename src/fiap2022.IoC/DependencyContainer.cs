
using fiap2022.persistence.Contexts;
using fiap2022.application.Interfaces;
using fiap2022.application.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using fiap2022.infrastructure.Clients;

namespace fiap2022.IoC
{
    public class DependencyContainer
    {

        public static void RegisterServices( IServiceCollection services, IConfigurationBuilder configuration)
        {


            //services.AddDataProtection()
            //    .SetApplicationName("fiap")
            //    .PersistKeysToFileSystem(new DirectoryInfo("C:\\Users\\rodolfofadino\\Desktop\\fiap2022"));

            var connection = @"Server=(localdb)\mssqllocaldb;Database=FiapDatabase;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<DataContext>
                (o => o.UseSqlServer(connection));

            services.AddControllersWithViews();
            //services.AddControllers();

            services.AddTransient<INoticiaService,NoticiaService>();
            services.AddTransient<INoticiasReader, NoticiasGloboRssClient>();

            services.Configure<RouteOptions>
                (options => options.LowercaseUrls = true);

            services.AddMemoryCache();

            services.Configure<GzipCompressionProviderOptions>(
                o => o.Level = System.IO.Compression.CompressionLevel.SmallestSize);

            services.AddResponseCompression(o =>
            {
                o.Providers.Add<GzipCompressionProvider>();
            });


            services.AddAuthentication("app")
                .AddCookie("app",
                o =>
                {
                    o.LoginPath = "/account/login";
                    o.AccessDeniedPath = "/account/denied";
                });


        }
    }
}
