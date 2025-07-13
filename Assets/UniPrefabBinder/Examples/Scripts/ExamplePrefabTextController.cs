using UniPrefabBinder.Main.Core.Attributes;
using UnityEngine;

namespace UniPrefabBinder.Examples.Scripts
{
    [PrefabPath("UniPrefabBinder/ExamplePrefabText")]
    public class ExamplePrefabTextController : MonoBehaviour
    {
        [ComponentBinding("MyText")] 
        private TextMesh _text = null!;

        private void Awake()
        {
            _text.text += "Binded";
        }
    }
}