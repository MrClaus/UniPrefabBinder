using System;

namespace UniPrefabBinder.Main.Core.Exception
{
    public class PrefabBindException : System.Exception
    {
        private const string NULL = "Attempt to Instantiate a prefab returned null object. Name={0}";
        private const string MISSING_TYPE = "Failed to bind prefab={0}. Not found type '{1}' for prefab binding.";
        private const string ATTRIBUTE = "Error getting prefab path by [PrefabPath] attribute";
        private const string COMPONENT = "Error binding component with name={0} in type={1}";
        private const string OBJECT = "Error binding object with name={0} in type={1}";
        private const string DEFAULT = "Error with status={0}";
        
        public PrefabBindException(PrefabBindErrorStatus status, string name = "", Type type = null) : base(GetMessage(status, name, type))
        {
        }
        
        public PrefabBindException(System.Exception e, string name = "") : base($"{e.Message}. Name={name}")
        {
        }

        private static string GetMessage(PrefabBindErrorStatus status, string name, Type type = null)
        {
            return status switch {
                PrefabBindErrorStatus.NULL => string.Format(NULL, name),
                PrefabBindErrorStatus.MISSING => string.Format(MISSING_TYPE, name, type == null ? "Undefined" : type.Name),
                PrefabBindErrorStatus.COMPONENT => string.Format(COMPONENT, name, type == null ? "Undefined" : type.Name),
                PrefabBindErrorStatus.OBJECT => string.Format(OBJECT, name, type == null ? "Undefined" : type.Name),
                PrefabBindErrorStatus.ATTRIBUTE => ATTRIBUTE,
                _ => string.Format(DEFAULT, status)
            };
        }
    }
}