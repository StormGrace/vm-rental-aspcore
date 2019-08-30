using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace vm_rental.Data
{
    public static class VmDbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetService<vmDbContext>();


                //context.SaveChanges();
            }
        }
    }
}
