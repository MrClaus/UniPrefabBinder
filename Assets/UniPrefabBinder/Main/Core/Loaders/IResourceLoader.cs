namespace UniPrefabBinder.Main.Core.Loaders
{
    public interface IResourceLoader
    {
        T Load<T>(string path)  where T : UnityEngine.Object;
    }
}