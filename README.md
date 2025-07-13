UniPrefabBinder
===
[![Releases](https://img.shields.io/github/release/MrClaus/UniPrefabBinder.svg)](https://github.com/MrClaus/UniPrefabBinder/releases)
[![Github all releases](https://img.shields.io/github/downloads/MrClaus/UniPrefabBinder/total.svg)](https://gitHub.com/MrClaus/UniPrefabBinder/releases/)

Library for fast prefab binding. Nice architectural solution through attributes.
Easily use binding attributes in your own component descriptions to describe the behavior of prefabs on the scene.

## Attributes used:
* `[ComponentBinding]` - for component binding
* `[ObjectBinding]` - for GameObject binding
* `[PrefabPath]` - to describe the path to your prefab in the `Resources` folder

```c#
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
```

The attribute's brackets indicate the object's name in the prefab hierarchy. If you want to get a component on the root object of the prefab itself, then simply write the attribute's name without brackets, as you can see in the example above.

If you have objects inside your prefab with other components implemented (which also have binding attributes), they will be automatically bound regardless of whether you access them by attributes or not. This recursive binding gives more freedom in implementing complex components for UI and other objects.

## Install via git URL
Requires a version of unity that supports path query parameter for git packages (Unity >= 2019.3.4f1, Unity >= 2020.1a21). You can add `https://github.com/MrClaus/UniPrefabBinder.git?path=Assets/UniPrefabBinder/Main` to Package Manager

![image](https://raw.githubusercontent.com/MrClaus/UniPrefabBinder/main/.github/images/upm_install_1.png)

![image](https://raw.githubusercontent.com/MrClaus/UniPrefabBinder/main/.github/images/upm_install_2.png)

or add `"com.github.mrclaus.uniprefabbinder": "https://github.com/MrClaus/UniPrefabBinder.git?path=Assets/UniPrefabBinder/Main"` to `Packages/manifest.json`.

If you want to set a target version, UniPrefabBinder uses the `*.*.*` release tag so you can specify a version like `#1.0.0`. For example `https://github.com/MrClaus/UniPrefabBinder.git?path=Assets/UniPrefabBinder/Main#1.0.0`.

## Install via Unity package file (.unitypackage)
The Unity package files will also contain `examples` of how to use the library. The package files (.unitypackage) for each version can be found in the [Releases](https://gitHub.com/MrClaus/UniPrefabBinder/releases/) section

![image](https://raw.githubusercontent.com/MrClaus/UniPrefabBinder/main/.github/images/upm_install_3.png)

## License
This library is under the MIT License.
