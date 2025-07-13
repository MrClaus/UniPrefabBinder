using System;
using JetBrains.Annotations;

namespace UniPrefabBinder.Main.Core.Attributes
{
    [PublicAPI]
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ComponentBindingAttribute : Attribute
    {
        [CanBeNull] 
        public string ComponentName { get; }
        
        public ComponentBindingAttribute(string componentName) => ComponentName = componentName;

        public ComponentBindingAttribute() : this(null)
        {
        }
    }
}