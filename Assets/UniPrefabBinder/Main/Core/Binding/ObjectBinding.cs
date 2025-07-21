using System.Reflection;
using UniPrefabBinder.Main.Core.Exception;
using UnityEngine;

namespace UniPrefabBinder.Main.Core.Binding
{
    internal class ObjectBinding : BindingModel, IBinding
    {
        public ObjectBinding(string name, MemberInfo memberInfo) : base(name, memberInfo)
        {
        }
        
        public void Bind(GameObject prefab, MonoBehaviour component, bool isParent = true)
        {
            GameObject child = GetChildByName(prefab, Name);
            if (child == null) {
                throw new PrefabBindException(PrefabBindErrorStatus.OBJECT, Name, component.GetType());
            }

            SetField(component, child);
        }
    }
}