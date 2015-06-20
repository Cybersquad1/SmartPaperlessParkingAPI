// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
// using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;

namespace SPP
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC to the services pipeline
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Use MVC in the application
            app.UseMvc();
        }
    }
}
