using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Common
{
    /// <summary>
    ///     Gets part of a stack trace containing only methods we care about.
    /// </summary>
    public class StackTraceHelper
    {
        private static readonly HashSet<string> AssembliesToExclude;
        private static readonly HashSet<string> TypesToExclude;
        private static readonly HashSet<string> MethodsToExclude;

        private const int StackMaxLength = 100;
        private const string AspNetEntryPointMethodName = "System.Web.HttpApplication.IExecutionStep.Execute";


        static StackTraceHelper()
        {
            TypesToExclude = new HashSet<string>
                {
                    // while we like our Dapper friend, we don't want to see him all the time
                    "SqlMapper"
                };

            MethodsToExclude = new HashSet<string>
                {
                    "lambda_method",
                    ".ctor"
                };

            AssembliesToExclude = new HashSet<string>
                {
                    // our assembly
                   typeof(Decompiler).Assembly.GetName().Name,

                    // reflection emit
                    "Anonymously Hosted DynamicMethods Assembly",

                    // the man
                    "System.Core",
                    "System.Data",
                    "System.Data.Entity",
                    "System.Data.Linq",
                    "System.Web",
                    "System.Web.Mvc",
                    "System.Windows.Forms",
                    "mscorlib",
                    "EntityFramework",
                };
        }

        /// <summary>
        ///     Gets the current formatted and filtered stack trace.
        /// </summary>
        /// <returns>Space separated list of methods</returns>
public static string Get(out StackFrame outStackFrame)
{
	outStackFrame = null;
	var frames = new StackTrace(0, true).GetFrames();
	if (frames == null)
	{
		return "";
	}

	var methods = new List<string>();

	// проходим по всем фреймам
	foreach (StackFrame t in frames)
	{
		// получаем метод
		var method = t.GetMethod();

		// получаем сборку и проверяем нужно ли ее пропустить
		var assembly = method.Module.Assembly.GetName().Name;
		if (ShouldExcludeType(method) || AssembliesToExclude.Contains(assembly) ||
			MethodsToExclude.Contains(method.Name))
			continue;

		// находим первый по стеку фрейм и считаем что именно он сгенерировал команду, если нет нужно добавить имя сборки в списк
		if (outStackFrame == null)
		{
			outStackFrame = t;
		}
		methods.Add(method.DeclaringType.FullName + ":" + method.Name);
	}
	return string.Join("\r\n", methods);
}

        private static bool ShouldExcludeType(MethodBase method)
        {
            var t = method.DeclaringType;
            while (t != null)
            {
                t = t.DeclaringType;
            }
            return false;
        }



        /// <summary>
        /// Excludes the specified assembly from the stack trace output.
        /// </summary>
        /// <param name="assemblyName">The short name of the assembly. AssemblyName.Name</param>
        public static void ExcludeAssembly(string assemblyName)
        {
            AssembliesToExclude.Add(assemblyName);
        }

        /// <summary>
        /// Excludes the specified type from the stack trace output.
        /// </summary>
        /// <param name="typeToExclude">The System.Type name to exclude</param>
        public static void ExcludeType(string typeToExclude)
        {
            TypesToExclude.Add(typeToExclude);
        }

        /// <summary>
        /// Excludes the specified method name from the stack trace output.
        /// </summary>
        /// <param name="methodName">The name of the method</param>
        public static void ExcludeMethod(string methodName)
        {
            MethodsToExclude.Add(methodName);
        }
    }
}