using System.Collections.Generic;
using System.Reflection;

namespace Csp
{
    public static class GlobalConfiguration
    {
        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }

        public static IList<Assembly> Modules { get; } = new List<Assembly>();

        public static void RegisterModule(Assembly assembly)
        {
            Modules.Add(assembly);
        }
    }
}
