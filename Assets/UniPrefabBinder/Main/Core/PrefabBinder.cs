using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UniPrefabBinder.Main.Core.Attributes;
using UniPrefabBinder.Main.Core.Binding;
using UniPrefabBinder.Main.Core.Exception;
using UniPrefabBinder.Main.Core.Loaders;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UniPrefabBinder.Main.Core
{
    public static class PrefabBinder
    {
        internal static Dictionary<Type, PrefabBinding> Binders { get; } = new Dictionary<Type, PrefabBinding>();
        public static LoaderSet LoaderSet { get; } = new LoaderSet();
        private static bool Inited { get; set; }

        public static T Instantiate<T>(Transform parent = null)
            where T : MonoBehaviour
        {
            string prefabPath = GetPrefabPath(typeof(T));
            InitBinders();

            GameObject prefab = LoaderSet.ResourceLoader.Load<GameObject>(prefabPath);
            if (prefab == null) {
                throw new PrefabLoadException(PrefabLoadErrorStatus.NULL, prefabPath);
            }
            
            return DoBind<T>(prefab, parent);
        }
        
        public static async Task<T> InstantiateAsync<T>(Transform parent = null)
            where T : MonoBehaviour
        {
            string prefabPath = GetPrefabPath(typeof(T));
            InitBinders();
            
            GameObject prefab = await LoaderSet.ResourceLoaderByTask.LoadAsync<GameObject>(prefabPath);
            if (prefab == null) {
                throw new PrefabLoadException(PrefabLoadErrorStatus.NULL, prefabPath);
            }

            return DoBind<T>(prefab, parent);
        }

        internal static T DoBind<T>(GameObject prefab, Transform parent = null)
            where T : MonoBehaviour
        {
            bool activeSelf = prefab.activeSelf;
            prefab.SetActive(false);
            GameObject instantiated;

            try {
                instantiated = Object.Instantiate(prefab, parent);
            }
            catch (System.Exception e) {
                throw new PrefabBindException(e, prefab.name);
            }
            
            if (instantiated == null) {
                throw new PrefabBindException(PrefabBindErrorStatus.NULL, prefab.name);
            }
            
            if (Binders.TryGetValue(typeof(T), out var binding)) {
                binding.Bind(instantiated);
                instantiated.SetActive(activeSelf);
                return instantiated.GetComponent<T>();
            }
            
            throw new PrefabBindException(PrefabBindErrorStatus.MISSING, prefab.name, typeof(T));
        }

        private static string GetPrefabPath(Type type)
        {
            PrefabPathAttribute prefabPathAttribute = type.GetCustomAttribute<PrefabPathAttribute>();
            string prefabPath = prefabPathAttribute?.PrefabPath;
            
            if (string.IsNullOrEmpty(prefabPath)) {
                throw new PrefabBindException(PrefabBindErrorStatus.ATTRIBUTE);
            }
            
            return prefabPath;
        }

        private static void InitBinders()
        {
            if (Inited) {
                return;
            }
            
            HashSet<Assembly> assemblies = new HashSet<Assembly> { Assembly.GetExecutingAssembly() };
            try {
                assemblies.Add(Assembly.Load("Assembly-CSharp"));
            } 
            catch {
                // ignored
            }

            foreach (Assembly assembly in assemblies) {
                foreach (Type type in assembly.GetTypes()) {
                    PrefabPathAttribute attribute = type.GetCustomAttribute<PrefabPathAttribute>();
                    if (attribute == null) {
                        continue;
                    }
                    
                    Binders[type] = new PrefabBinding(type);
                }
            }
            
            LoaderSet.TryInit();
            Inited = true;
        }
    }
}