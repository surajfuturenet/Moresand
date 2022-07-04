using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ImageProcessService
{
    public class Startup
    {
        //THis is where all DLL are stored
        
        List<AssemblyPart> plugins = new List<AssemblyPart>();
       

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);

            LoadPlugins();

            //get the list of assemblies and register their controllers.
            foreach (AssemblyPart assemblyPart in  plugins)
            {
                services.AddControllers().PartManager.ApplicationParts.Add(assemblyPart);
            }

        }


        /// <summary>
        /// Load the Listed dlls from the path specified. 
        /// </summary>
        void LoadPlugins()
        {

            string plugin_path = Configuration.GetSection("pluginspath").Value;
            try
            {
                foreach (var dll in Directory.GetFiles(plugin_path, "*.dll"))
                {
                    Assembly assembly =  Assembly.LoadFrom(dll);
                    plugins.Add(new AssemblyPart(assembly));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
