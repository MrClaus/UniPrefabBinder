using UnityEngine;

namespace UniPrefabBinder.Main.Core.Binding
{
    public interface IBinding
    {
        void Bind(GameObject prefab, MonoBehaviour component, bool isParent);
    }
}