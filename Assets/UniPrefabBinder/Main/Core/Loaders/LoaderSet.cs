using UniPrefabBinder.Main.Core.Exception;
using UniPrefabBinder.Main.Core.Loaders.Default;

namespace UniPrefabBinder.Main.Core.Loaders
{
    public sealed class LoaderSet
    {
        private IResourceLoader _resourceLoader;
        private IResourceLoaderByTask _resourceLoaderByTask;

        internal IResourceLoader ResourceLoader => CheckNullInterface(_resourceLoader);
        internal IResourceLoaderByTask ResourceLoaderByTask => CheckNullInterface(_resourceLoaderByTask);
        
        public LoaderSet Add(IResourceLoader loader)
        {
            _resourceLoader = loader;
            return this;
        }
        
        public LoaderSet Add(IResourceLoaderByTask loader)
        {
            _resourceLoaderByTask = loader;
            return this;
        }

        public LoaderSet Reset()
        {
            _resourceLoader = null;
            _resourceLoaderByTask = null;
            return this;
        }

        public void DefaultInit()
        {
            var defaultLoader = new PrefabLoader();
            
            Reset();
            Add((IResourceLoader) defaultLoader);
            Add((IResourceLoaderByTask) defaultLoader);
        }
        
        internal void TryInit()
        {
            var defaultLoader = new PrefabLoader();

            if (_resourceLoader == null) {
                Add((IResourceLoader) defaultLoader);
            }
            if (_resourceLoaderByTask == null) {
                Add((IResourceLoaderByTask) defaultLoader);
            }
        }

        private T CheckNullInterface<T>(T implemented)
        {
            if (implemented == null) {
                throw new PrefabLoadException(PrefabLoadErrorStatus.IMPLEMENTATION, typeof(T));
            }

            return implemented;
        }
    }
}