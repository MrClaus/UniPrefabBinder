using System;
using System.Reflection;
using UniPrefabBinder.Main.Core.Exception;
using UnityEngine;

namespace UniPrefabBinder.Main.Core.Binding
{
    internal class ComponentBinding : BindingModel, IBinding
    {
        private Type ComponentType { get; }

        public ComponentBinding(string name, MemberInfo memberInfo, Type componentType) : base(name, memberInfo)
        {
            ComponentType = componentType;
        }

        public void Bind(GameObject prefab, MonoBehaviour component, bool isParent = true)
        {
            if (!typeof(Component).IsAssignableFrom(ComponentType)) {
                throw new PrefabBindException(PrefabBindErrorStatus.COMPONENT, ComponentType.Name, component.GetType());
            }

            Component childComponent = (Name == null ? prefab : GetChildByName(prefab, Name))?.GetComponent(ComponentType);
            if (childComponent == null) {
                throw new PrefabBindException(PrefabBindErrorStatus.COMPONENT, ComponentType.Name, component.GetType());
            }
            
            SetField(component, childComponent);
        }
    }
}