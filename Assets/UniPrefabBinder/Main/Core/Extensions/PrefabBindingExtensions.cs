using UniPrefabBinder.Main.Core.Exception;
using UnityEngine;

namespace UniPrefabBinder.Main.Core.Extensions
{
    public static class PrefabBindingExtensions
    {
        public static T TryBind<T>(this GameObject prefab, Transform parent = null)
            where T : MonoBehaviour
        {
            var component = prefab?.GetComponent<T>();
            if (component == null) {
                throw new PrefabBindException(PrefabBindErrorStatus.MISSING, prefab?.name, typeof(T));
            }
            
            return PrefabBinder.DoBind<T>(prefab, parent);
        }
    }
}