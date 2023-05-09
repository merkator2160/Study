using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using SerializationTask.Common.Contracts.Exceptions.Application;
using System.Reflection;

namespace SerializationTask.Common.DependencyInjection
{
    public static class Collector
    {
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
        public static void RegisterConfiguration(this ContainerBuilder builder, IConfiguration configuration, Assembly[] assembliesToScan)
        {
            foreach (var assembly in assembliesToScan)
            {
                builder.RegisterConfiguration(configuration, assembly);
            }
        }
        public static void RegisterLocalConfiguration(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterConfiguration(configuration, Assembly.GetCallingAssembly());
        }
        public static void RegisterCommonConfiguration(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterConfiguration(configuration, Assembly.GetExecutingAssembly());
        }
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
        public static void RegisterConfiguration(this ContainerBuilder builder, IConfiguration configuration, Assembly assemblyToScan)
        {
            var configTypes = assemblyToScan.DefinedTypes.Where(p => p.IsClass && p.Name.EndsWith("Config")).ToArray();
            foreach (var x in configTypes)
            {
                var configInstance = configuration.GetSection(x.Name).Get(x);
                if (configInstance != null)
                {
                    builder.RegisterInstance(configInstance).AsSelf();
                }
            }
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