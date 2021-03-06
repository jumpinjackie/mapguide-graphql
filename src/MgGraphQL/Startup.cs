using Autofac;
using GraphQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using GraphQL.Types;
using MgGraphQL.Controllers;
using MgGraphQL.GraphModel;
using MgGraphQL.GraphModel.Output;
using MgGraphQL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OSGeo.MapGuide;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MgGraphQL
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                builder.AddJsonFile("appsettings.windows.json");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                builder.AddJsonFile("appsettings.linux.json");
            }
            else
            {
                throw new NotSupportedException("MapGuide doesn't work on this platform");
            }

            // Set up configuration sources.
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            string mgWebConfigPath = Configuration["MapGuide.WebConfigPath"];
            MapGuideApi.MgInitializeWebTier(mgWebConfigPath);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>();
            builder.RegisterType<GraphQLQuery>().AsSelf();
            builder.RegisterType<GraphQLMutation>().AsSelf();
            builder.RegisterType<GraphQLSchema>().As<ISchema>();
            builder.Register<Func<Type, GraphType>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return t => {
                    var res = context.Resolve(t);
                    return (GraphType)res;
                };
            });

            //Auto-wire all resolver implementations
            var resolverAssembly = typeof(IQueryResolver).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(resolverAssembly)
                .Where(t => t.Name.EndsWith("Resolver"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Auto-wire all IGraphQLType implementations
            var graphTypesAssembly = typeof(IGraphQLType).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(graphTypesAssembly)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsAssignableFrom(typeof(IGraphQLType))))
                .AsSelf();

            builder.RegisterGeneric(typeof(ResponseGraphType<>)).AsSelf();
            builder.RegisterGeneric(typeof(ResponseListGraphType<>)).AsSelf();

            //Auto-wire all service implementations
            var serviceAssembly = typeof(ResourceService).Assembly;
            builder.RegisterAssemblyTypes(serviceAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/graphql/playground", GraphQLEndPoint = $"/{GraphQLController.API_ENDPOINT}" });
                app.UseGraphQLVoyager(new GraphQLVoyagerOptions() { Path = "/graphql/voyager", GraphQLEndPoint = $"/{GraphQLController.API_ENDPOINT}" });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
