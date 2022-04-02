using First.App.Business.Abstract;
using First.App.Business.Concretes;
using First.App.DataAccess.EntityFramework;
using First.App.DataAccess.EntityFramework.Repository.Abstracts;
using First.App.DataAccess.EntityFramework.Repository.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GetPostsBackgroundWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AppDbContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("DBConnection")));
                    // add services to container
                    services.AddTransient<IPostService, PostService>();
                    services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                    services.AddHostedService<Worker>();
                });
    }
}
