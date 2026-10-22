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



        public void Log(string message)
        {
            #if UNITY_EDITOR
            Debug.Log($"[<color=#{TitleColor.ToHexString()}>{Title}</color>] " +
                      $"<color=#{MessageColor.ToHexString()}>{message}</color>");
            #endif
        }

        public void LogWarning(string message)
        {
            #if UNITY_EDITOR
            Debug.LogWarning($"[<color=#{TitleColor.ToHexString()}>{Title}</color>] " +
                             $"<color=#{MessageColor.ToHexString()}>{message}</color>");
            #endif
        }


        public void LogError(string message)
        {
            #if UNITY_EDITOR
            Debug.LogError($"[<color=#{TitleColor.ToHexString()}>{Title}</color>] " +
                           $"<color=#{MessageColor.ToHexString()}>{message}</color>");
            #endif
        }
    }
}