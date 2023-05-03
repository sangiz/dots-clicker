using UnityEngine;

namespace IgnSDK.Infrastructure.Services
{
    public interface IAssets
    {
	    T Instantiate<T>(GameObject prefab) where T : MonoBehaviour;
        T Instantiate<T>(GameObject prefab, Vector3 at, Transform parent = null) where T : MonoBehaviour;
        T GetPrefab<T>(string path) where T : MonoBehaviour;
        T GetScriptableObject<T>(string path) where T : ScriptableObject;
    }
}