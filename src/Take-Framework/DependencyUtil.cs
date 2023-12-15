using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TakeFramework
{
    /// <summary>
    /// DependencyUtilities
    /// </summary>
    public sealed partial class DependencyUtil
    {
        const string pattern = "^Microsoft.\\w*|^System.\\w*|^Newtonsoft.\\w*|^Autofac.\\w*|^Serilog.\\w*|^App.\\w*|^runtime.\\w*|^Google.\\w*|^IdentityServer4.\\w*|^EntityFramework.\\w*" +
            "|^Azure.Cosmos.\\w*|^Spire.\\w*";

        private static Assembly[] assemblies = [];
        /// <summary>
        /// GetReferencedAssemblies
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetReferencedAssemblies(bool refresh = false)
        {
            Regex relatedRegex = MyRegex();
            if (DependencyContext.Default == null)
            {
                assemblies = [];
            }
            else if (refresh || assemblies.Length == 0)
            {
                assemblies = DependencyContext.Default.RuntimeLibraries.Where(item => !relatedRegex.IsMatch(item.Name)).SelectMany(item => item.GetDefaultAssemblyNames(DependencyContext.Default)).Select(Assembly.Load).ToArray();
            }
            return assemblies;
        }

        [GeneratedRegex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline, "zh-CN")]
        private static partial Regex MyRegex();
        public static string GetInterfaceName(string implementationName) => $"I{implementationName}";
    }


}
