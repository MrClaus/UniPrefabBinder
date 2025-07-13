using System;
using JetBrains.Annotations;

namespace UniPrefabBinder.Main.Core.Attributes
{
    [PublicAPI]
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ObjectBindingAttribute : Attribute
    {
        public string ObjectName { get; }

        public ObjectBindingAttribute(string objectName) => ObjectName = objectName;
    }
}