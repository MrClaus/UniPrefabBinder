using System.Threading.Tasks;
using UnityEngine;
using UniPrefabBinder.Main.Core;

namespace UniPrefabBinder.Examples.Scripts
{
    public class ExampleForScene : MonoBehaviour
    {
        private void Start()
        {
            Bind();
        }
        
        private void Bind()
        {
            ExamplePrefabController prefab = PrefabBinder.Instantiate<ExamplePrefabController>(transform);
            Debug.Log($"Binded prefab name={prefab.name}");
        }

        private async Task BindAsync()
        {
            ExamplePrefabController prefab = await PrefabBinder.InstantiateAsync<ExamplePrefabController>(transform);
            Debug.Log($"Binded (async) prefab name={prefab.name}");
        }
    }
}