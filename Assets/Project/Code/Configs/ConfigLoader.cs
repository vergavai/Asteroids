using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Project.Code.Configs
{
    public class ConfigLoader
    {
        public static T LoadConfig<T>(string resourcePath) where T : class
        {
            TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);
            if (textAsset == null)
            {
                Debug.LogError($"Config file not found in Resources: {resourcePath}");
                return null;
            }

            try
            {
                T config = JsonConvert.DeserializeObject<T>(textAsset.text);
                if (config == null)
                {
                    Debug.LogError($"Deserialization returned null for {resourcePath}");
                }
                return config;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to deserialize config {resourcePath}: {ex.Message}");
                return null;
            }
        }
    }
}