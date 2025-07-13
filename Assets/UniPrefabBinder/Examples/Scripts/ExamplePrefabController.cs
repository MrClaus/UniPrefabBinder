using UniPrefabBinder.Main.Core.Attributes;
using UnityEngine;

namespace UniPrefabBinder.Examples.Scripts
{
    [PrefabPath("UniPrefabBinder/ExamplePrefab")]
    public class ExamplePrefabController : MonoBehaviour
    {
        [ComponentBinding("Text")]
        private TextMesh _text = null!;
        
        [ComponentBinding("Sphere")]
        private MeshRenderer _sphereMat = null!;
        
        [ComponentBinding]
        private MeshRenderer _quad = null!;
        
        [ObjectBinding("Sphere")]
        private GameObject _sphere = null!;

        [ObjectBinding("ExamplePrefabText")]
        private GameObject _pfText = null!;

        private float _time;

        private void Awake()
        {
            _text.text = "Binded";
            _quad.enabled = false;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= 1) {
                _sphere.SetActive(!_sphere.activeSelf);
                _pfText.SetActive(!_pfText.activeSelf);
                _sphereMat.material.color = Color.red;
                _time = 0;
            }
        }
    }
}