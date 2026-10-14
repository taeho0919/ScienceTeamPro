using Unity.VisualScripting;
using UnityEngine;

namespace _01.Scripts.Core.Logger
{
    [CreateAssetMenu(fileName = "Logger", menuName = "Core/Scriptable Logger", order = 0)]
    public class ScriptableLogger : ScriptableObject, ILogger
    {
        [field:Header("ScriptableLogger Setting")]
        [field:SerializeField] public string Title { get; private set; }
        [field:SerializeField] public Color TitleColor { get; private set; }
        [field:SerializeField] public Color MessageColor { get; private set; }


        
        #if UNITY_EDITOR
        public void Log(string message)
        {
            Debug.Log($"[<color=#{TitleColor.ToHexString()}>{Title}</color>] " +
                      $"<color=#{MessageColor.ToHexString()}>{message}</color>");
        }
        #endif

        #if UNITY_EDITOR
        public void LogWarning(string message)
        {
            Debug.LogWarning($"[<color=#{TitleColor.ToHexString()}>{Title}</color>] " +
                             $"<color=#{MessageColor.ToHexString()}>{message}</color>");
        }
        #endif

        
        #if UNITY_EDITOR
        public void LogError(string message)
        {
            Debug.LogError($"[<color=#{TitleColor.ToHexString()}>{Title}</color>] " +
                           $"<color=#{MessageColor.ToHexString()}>{message}</color>");
        }
        #endif
    }
}