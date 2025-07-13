using UnityEngine;
using UniPrefabBinder.Main.Core;

namespace UniPrefabBinder.Examples.Scripts
{
    public class ExampleForScene : MonoBehaviour
    {
        private void Start()
        {
            ExamplePrefabController prefab = PrefabBinder.Instantiate<ExamplePrefabController>(transform);
            Debug.Log($"Binded prefab name={prefab.name}");
        }
    }
}