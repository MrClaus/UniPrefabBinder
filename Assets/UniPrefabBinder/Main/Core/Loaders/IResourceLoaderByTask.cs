using System.Threading.Tasks;

namespace UniPrefabBinder.Main.Core.Loaders
{
    public interface IResourceLoaderByTask
    {
        Task<T> LoadAsync<T>(string path) where T : UnityEngine.Object;
    }
}