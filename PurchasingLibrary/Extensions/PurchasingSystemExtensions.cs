using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PurchasingSystem.BLL;
using PurchasingSystem.Models;
namespace PurchasingSystem.Extensions
{
    public static class PurchasingSystemExtensions
    {
        public static void AddPurchasingSystemDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<eBike_2026CleanContext>(options);
            services.AddTransient<PurchasingService>();
        }
    }
}
