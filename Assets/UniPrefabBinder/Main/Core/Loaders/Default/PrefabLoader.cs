using System;
using System.Threading;
using System.Threading.Tasks;
using UniPrefabBinder.Main.Core.Exception;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UniPrefabBinder.Main.Core.Loaders.Default
{
    internal class PrefabLoader : IResourceLoader, IResourceLoaderByTask
    {
        internal const int TIMEOUT = 60;
        
        public T Load<T>(string path) where T : Object => ExtractPrefab<T>(Resources.Load<T>(path));

        public async Task<T> LoadAsync<T>(string path) where T : Object
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            TaskCompletionSource<T> operationSource = new();
            var request = Resources.LoadAsync<T>(path);
            T result;

            if (request.isDone) {
                operationSource.TrySetResult(ExtractPrefab<T>(request.asset));
            }
            else {
                request.completed += _ => operationSource.TrySetResult(ExtractPrefab<T>(request.asset));
            }
            
            try {
                cancellationToken.CancelAfter(TimeSpan.FromSeconds(TIMEOUT));
                result = await operationSource.Task;
            }
            catch (OperationCanceledException) {
                throw new PrefabLoadException(PrefabLoadErrorStatus.CANCELLED, path);
            }
            catch (System.Exception e) {
                throw new PrefabLoadException(e, path);
            }
            finally {
                cancellationToken.Dispose();
            }

            return result;
        }
        
        private T ExtractPrefab<T>(Object asset) where T : Object => asset is GameObject prefab ? prefab as T : null;
    }
}