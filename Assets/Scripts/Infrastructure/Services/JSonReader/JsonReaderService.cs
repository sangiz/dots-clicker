using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.JSonReader
{
    public class JsonReaderService : IJsonReaderService
    {
        public T ReadData<T>(string path)
        {
            try
            {
                var jsonFile = Resources.Load<TextAsset>(path);
                var result = JsonUtility.FromJson<T>(jsonFile.text);

                return result;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error reading JSON data: " + ex.Message);
            }

            return default;
        }
    }
}
