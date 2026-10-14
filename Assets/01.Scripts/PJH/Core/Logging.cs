using UnityEngine;

namespace _01.Scripts.Core
{
    
    public static class Logging
    {
        #if UNITY_EDITOR
        public static void Log(object message)
        {
            Debug.Log(message);
        }
        #endif

        #if UNITY_EDITOR
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }
        #endif
        
        #if UNITY_EDITOR
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }
        #endif
        
    }
}