using System;
using System.Collections.Generic;
using System.Reflection;

namespace BlueMonkey
{
    public static class PageNavigationTypeResolver
    {
        private static readonly Dictionary<Assembly, Assembly> ViewModelAssignedToViewAssemblies = new Dictionary<Assembly, Assembly>();

        public static void AssignAssemblies<TView, TViewModel>()
        {
            var viewAssembly = typeof(TView).GetTypeInfo().Assembly;
            var viewModelAssembly = typeof(TViewModel).GetTypeInfo().Assembly;
            ViewModelAssignedToViewAssemblies[viewAssembly] = viewModelAssembly;
        }

        public static Type ResolveForViewModelType(Type viewType)
        {
            if (viewType == null) throw new ArgumentNullException(nameof(viewType));

            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
            var assembly = ResolveAssembly(ViewModelAssignedToViewAssemblies, viewType.GetTypeInfo().Assembly);
            return assembly.GetType($"{viewName}{suffix}");
        }

        public static void Clear()
        {
            ViewModelAssignedToViewAssemblies.Clear();
        }

        private static Assembly ResolveAssembly(Dictionary<Assembly, Assembly> assemblies, Assembly key)
        {
            Assembly result;
            if (!assemblies.TryGetValue(key, out result))
            {
                result = key;
                assemblies[key] = result;
            }
            return result;
        }
    }
}
