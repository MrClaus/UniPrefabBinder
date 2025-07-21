using System;
using UniPrefabBinder.Main.Core.Loaders;
using UniPrefabBinder.Main.Core.Loaders.Default;

namespace UniPrefabBinder.Main.Core.Exception
{
    public class PrefabLoadException : System.Exception
    {
        private const string CANCELLED = "Prefab load time exceeded {0} seconds. Path={1}";
        private const string NULL = "Attempt to Load a prefab returned null object. Path={0}";
        private const string IMPLEMENTATION = "{0} in {1} not implemented!";
        private const string DEFAULT = "Error with status={0}";
        
        public PrefabLoadException(PrefabLoadErrorStatus status, string path) : base(GetMessage(status, path))
        {
        }
        
        public PrefabLoadException(PrefabLoadErrorStatus status, Type type) : base(GetMessage(status, type: type))
        {
        }
        
        public PrefabLoadException(System.Exception e, string path) : base($"{e.Message}. Path={path}")
        {
        }

        private static string GetMessage(PrefabLoadErrorStatus status, string path = "", Type type = null)
        {
            return status switch {
                PrefabLoadErrorStatus.CANCELLED => string.Format(CANCELLED, PrefabLoader.TIMEOUT, path),
                PrefabLoadErrorStatus.NULL => string.Format(NULL, path),
                PrefabLoadErrorStatus.IMPLEMENTATION => string.Format(IMPLEMENTATION, type == null ? "Undefined" : type.Name, nameof(LoaderSet)),
                _ => string.Format(DEFAULT, status)
            };
        }
    }
}