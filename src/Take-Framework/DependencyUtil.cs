using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TakeFramework
{
    /// <summary>
    /// DependencyUtilities
    /// </summary>
    public sealed class DependencyUtil
    {
        const string pattern = "^Microsoft.\\w*|^System.\\w*|^Newtonsoft.\\w*|^Autofac.\\w*|^Serilog.\\w*|^App.\\w*|^runtime.\\w*|^Google.\\w*|^IdentityServer4.\\w*|^EntityFramework.\\w*" +
            "|^Azure.Cosmos.\\w*|^Spire.\\w*";

        /// <summary>
        /// GetReferencedAssemblies
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetReferencedAssemblies()
        {
            Regex relatedRegex = new(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return DependencyContext.Default == null ? Array.Empty<Assembly>() : DependencyContext.Default.RuntimeLibraries
                 .Where(item => !relatedRegex.IsMatch(item.Name))
                 .SelectMany(item => item.GetDefaultAssemblyNames(DependencyContext.Default))
                 .Select(item => Assembly.Load(item))
                 .ToArray();
        }
    }
}
