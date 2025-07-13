using System;
using JetBrains.Annotations;

namespace UniPrefabBinder.Main.Core.Attributes
{
    [PublicAPI]
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabPathAttribute : Attribute
    {
        public string PrefabPath { get; }

        public PrefabPathAttribute(string prefabPath) => PrefabPath = prefabPath;
    }
}