using IgnSDK.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Services.AssetManagement
{
    public class Assets : IAssets
    {
        private readonly IInstantiator instantiator;

        public Assets(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public T GetPrefab<T>(string path) where T : MonoBehaviour
        {
            var prefab = Resources.Load<T>(path);

            if (prefab == null)
            {
                Debug.LogError($"Couldn't load prefab from resources by path: {path}");
            }

            return prefab;
        }

        public T GetScriptableObject<T>(string path) where T : ScriptableObject
        {
            var prefab = Resources.Load<T>(path);

            if (prefab == null)
            {
                Debug.LogError($"Couldn't load scriptable object from path: {path}");
            }

            return prefab;
        }

        public T Instantiate<T>(GameObject prefab, Vector3 at, Transform parent = null) where T : MonoBehaviour
        {
            var instantiated = instantiator.InstantiatePrefabForComponent<T>(prefab, parent);
            instantiated.name = instantiated.name.Replace("(Clone)", string.Empty);
            var transform = instantiated.transform;
            transform.position = at;
            transform.rotation = Quaternion.identity;
            return instantiated;
        }

        public T Instantiate<T>(GameObject prefab) where T : MonoBehaviour
        {
            var instantiated = instantiator.InstantiatePrefabForComponent<T>(prefab);
            instantiated.name = instantiated.name.Replace("(Clone)", string.Empty);
            var transform = instantiated.transform;
            transform.rotation = Quaternion.identity;
            return instantiated;
        }
    }
}