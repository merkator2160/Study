using Autofac;
using Common.Contracts.Exceptions.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Common.DependencyInjection
{
    public static class Collector
    {
        // Services
        public static void RegisterLocalServices(this ContainerBuilder builder)
        {
            builder.RegisterServices(Assembly.GetCallingAssembly());
        }
        public static void RegisterServices(this ContainerBuilder builder, Assembly[] assembliesToScan)
        {
            foreach (var assembly in assembliesToScan)
            {
                builder.RegisterServices(assembly);
            }
        }
        public static void RegisterServices(this ContainerBuilder builder, Assembly assemblyToScan)
        {
            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(p => p.IsClass && p.Name.EndsWith("Service"))
                .AsSelf()
                .AsImplementedInterfaces();
        }

        // Hangfire //
        public static void RegisterLocalHangfireJobs(this ContainerBuilder builder)
        {
            builder.RegisterHangfireJobs(Assembly.GetCallingAssembly());
        }
        public static void RegisterHangfireJobs(this ContainerBuilder builder, Assembly[] assembliesToScan)
        {
            foreach (var assembly in assembliesToScan)
            {
                builder.RegisterHangfireJobs(assembly);
            }
        }
        public static void RegisterHangfireJobs(this ContainerBuilder builder, Assembly assemblyToScan)
        {
            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(p => p.IsClass && p.Name.EndsWith("Job"))
                .AsSelf()
                .AsImplementedInterfaces();
        }

        // Configuration
        public static void RegisterLocalConfiguration(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterConfiguration(configuration, Assembly.GetCallingAssembly());
        }
        public static void RegisterCommonConfiguration(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterConfiguration(configuration, Assembly.GetExecutingAssembly());
        }
        public static void RegisterConfiguration(this ContainerBuilder builder, IConfiguration configuration, Assembly[] assembliesToScan)
        {
            foreach (var assembly in assembliesToScan)
            {
                builder.RegisterConfiguration(configuration, assembly);
            }
        }
        public static void RegisterConfiguration(this ContainerBuilder builder, IConfiguration configuration, Assembly assemblyToScan)
        {
            //Console.WriteLine($"{assemblyToScan.GetName()}:");
            var configTypes = assemblyToScan.DefinedTypes.Where(p => p.IsClass && p.Name.EndsWith("Config")).ToArray();
            foreach (var x in configTypes)
            {
                //Console.WriteLine(x.Name);
                var configInstance = configuration.GetSection(x.Name).Get(x);
                if (configInstance != null)
                {
                    //Console.WriteLine(x.Name);
                    builder.RegisterInstance(configInstance).AsSelf();
                }
            }
        }

        // Forms
        public static void RegisterLocalForms(this ContainerBuilder builder)
        {
            builder.RegisterForms(Assembly.GetCallingAssembly());
        }
        public static void RegisterForms(this ContainerBuilder builder, Assembly[] assembliesToScan)
        {
            foreach (var x in assembliesToScan)
            {
                builder.RegisterForms(x);
            }
        }
        public static void RegisterForms(this ContainerBuilder builder, Assembly assemblyToScan)
        {
            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(p => p.IsClass && p.Name.EndsWith("Form"))
                .AsSelf();
        }

        // View models
        public static void RegisterLocalViewModels(this ContainerBuilder builder)
        {
            builder.RegisterViewModels(Assembly.GetCallingAssembly());
        }
        public static void RegisterViewModels(this ContainerBuilder builder, Assembly[] assembliesToScan)
        {
            foreach (var x in assembliesToScan)
            {
                builder.RegisterViewModels(x);
            }
        }
        public static void RegisterViewModels(this ContainerBuilder builder, Assembly assemblyToScan)
        {
            builder
                .RegisterAssemblyTypes(assemblyToScan)
                .Where(p => p.IsClass && p.Name.EndsWith("Vm"))
                .AsSelf();
        }

        // Other
        public static Assembly[] LoadAssemblies(params String[] partsOfName)
        {
            var assemblyList = new List<Assembly>();
            foreach (var x in partsOfName)
                assemblyList.AddRange(LoadAssemblies(x));

            return assemblyList.ToArray();
        }
        public static Assembly[] LoadAssemblies(String partOfName)
        {
            Assembly[] assemblies;

            // DependencyContext.Default in Blazor is null
            if (DependencyContext.Default != null)
            {
                // Not all assemblies can be loaded by AppDomain.CurrentDomain.GetAssemblies()
                assemblies = DependencyContext.Default.RuntimeLibraries
                    .Where(d => d.Name.Contains(partOfName))
                    .Select(p => Assembly.Load(new AssemblyName(p.Name)))
                    .ToArray();
            }
            else
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(d => d.GetName().Name.Contains(partOfName))
                    .ToArray();
            }

            if (assemblies.Length == 0)
                throw new AssemblyNotFoundException($"No assemblies were found with part of name: \"{partOfName}\" ");

            return assemblies;
        }
        public static Assembly GetAssembly(String name)
        {
            return Assembly.Load(new AssemblyName(DependencyContext.Default.RuntimeLibraries.First(p => p.Name.Equals(name)).Name));
        }
    }
}